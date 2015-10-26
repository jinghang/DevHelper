using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using log4net;
using Word= Microsoft.Office.Interop.Word;
using Task = System.Threading.Tasks.Task;

namespace ADH.DevHelperWeb.App_Code
{
    public class ConvertMgr
    {
        private static readonly string SwfToool = ConfigurationManager.AppSettings["SwfTool"];
        private static readonly ILog Log = LogManager.GetLogger(typeof (ConvertMgr));

        public static void AddToQueue(string destDir,string srcFile)
        {
            Log.Debug("destDir:"+destDir);
            Log.Debug("srcFile:"+srcFile);
            if (!Directory.Exists(destDir) || !File.Exists(srcFile))
            {
                Log.Debug("参数不符合，退出");
                return;
            }
            Task.Factory.StartNew(
                () =>
                {
                    Log.Debug("启动新线程，srcFile:"+srcFile);
                    //如果是WORD文档
                    var fileExt = Path.GetExtension(srcFile)??"".ToLower();
                    if (fileExt == ".doc" || fileExt == ".docx")
                    {
                        Log.Debug("准备将Word文件转换成PDF文件");
                        var pdfFile = srcFile.Substring(0, srcFile.LastIndexOf('.')) + ".pdf";
                        Log.Debug("准备转换成PDF,pdfFile:"+pdfFile);
                        try
                        {
                            Word2Pdf(srcFile, pdfFile);
                            Log.Debug("将Word转换成PDF成功: "+pdfFile);
                        }
                        catch (Exception e)
                        {
                            Log.Error("Word转换成PDF出错，错误信息："+e.Message);
                            return;
                        }
                        
                        srcFile = pdfFile;
                    }
                    
                    //将PDF传成SWF
                    Log.Debug("准备转换成SWF");
                    var srcSwfFile = srcFile.Substring(0, srcFile.LastIndexOf('.')) + ".swf";
                    var arg = string.Format("\"{0}\" -f -s flashversion=9 -o \"{1}\"", srcFile, srcSwfFile); ;
                    Log.Debug("SwfTool参数: " + arg);
                    var process = new Process
                             {
                                 StartInfo = new ProcessStartInfo(SwfToool, arg)
                                             {
                                                 UseShellExecute = false,
                                                 WindowStyle = ProcessWindowStyle.Hidden,
                                                 CreateNoWindow = true,
                                                 RedirectStandardError = true,
                                                 RedirectStandardOutput = true
                                             }
                             };
                    try
                    {
                        process.Start();
                        var output = process.StandardOutput.ReadToEnd();
                        //process.StandardError
                        Log.Debug("SwfTool转换器输出数据：\n"+output);
                        //var 
                        process.WaitForExit();
                        Log.Debug("将PDF文件转换成SWF文件成功： "+srcSwfFile);
                    }
                    catch (Exception e)
                    {
                        Log.Error("将PDF转换成SWF出错，错误信息："+e.Message);
                        return;
                    }
                    

                    try
                    {
                        if (File.Exists(srcSwfFile))
                        {
                            var destFile = Path.Combine(destDir, Path.GetFileName(srcSwfFile));
                            File.Copy(srcSwfFile, destFile);
                            File.Delete(srcSwfFile);
                            //File.Delete(srcFile);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error("复制SWF文件出错，错误信息："+e.Message);
                    }
                    

                 });
        }

        public static void Word2Pdf(string path, string savepath)
        {
            var word = new Word.ApplicationClass();
            var wordType = word.GetType();
            var docs = word.Documents;
            var docsType = docs.GetType();
            var doc = (Word.Document)docsType.InvokeMember(
                "Open",
                BindingFlags.InvokeMethod,
                null,
                docs,
                new[] { (object)path, true, true });
            var docType = doc.GetType();
            docType.InvokeMember(
                "SaveAs",
                BindingFlags.InvokeMethod,
                null,
                doc,
                new[] { (object)savepath, Word.WdSaveFormat.wdFormatPDF });
            docType.InvokeMember(
                "Close",
                BindingFlags.InvokeMethod,
                null,
                doc,
                new object[] { Word.WdSaveOptions.wdDoNotSaveChanges });
            wordType.InvokeMember(
                "Quit",
                BindingFlags.InvokeMethod,
                null,
                word,
                null);
        }

    }
}