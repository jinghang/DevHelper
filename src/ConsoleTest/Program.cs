using System.Reflection;
using Microsoft.Office.Interop.Word;

namespace ConsoleTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var srcDoc = @"E:\Dev\Demand\爱店汇\26_微商城_品牌新用户活动需求\微商城_品牌新用户活动需求.doc";
            var dstDoc = @"E:\Dev\Demand\爱店汇\26_微商城_品牌新用户活动需求\微商城_品牌新用户活动需求.pdf";
            Word2Pdf(srcDoc, dstDoc);
        }

        public static void Word2Pdf(string path, string savepath)
        {
            var word = new ApplicationClass();
            var wordType = word.GetType();
            var docs = word.Documents;
            var docsType = docs.GetType();
            var doc = (Document) docsType.InvokeMember(
                "Open",
                BindingFlags.InvokeMethod,
                null,
                docs,
                new[] {(object)path, true, true});
            var docType = doc.GetType();
            docType.InvokeMember(
                "SaveAs",
                BindingFlags.InvokeMethod,
                null,
                doc,
                new[] {(object) savepath, WdSaveFormat.wdFormatPDF});
            docType.InvokeMember(
                "Close",
                BindingFlags.InvokeMethod,
                null,
                doc,
                new object[] {WdSaveOptions.wdDoNotSaveChanges});
            wordType.InvokeMember(
                "Quit",
                BindingFlags.InvokeMethod,
                null,
                word,
                null);
        }
    }
}