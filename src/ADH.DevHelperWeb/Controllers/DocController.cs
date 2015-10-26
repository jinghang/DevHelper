using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using ADH.DevHelperWeb.App_Code;
using ADH.DevHelperWeb.Models;
using log4net;

namespace ADH.DevHelperWeb.Controllers
{
    public class DocController : Controller
    {
        private readonly string _rootPath = System.Web.HttpContext.Current.Server.MapPath("~/");
        private readonly string _docPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/"), "docs");
        private readonly string _docCachePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/"), "docsCache");

        private readonly ILog _log = LogManager.GetLogger(typeof (DocController));

        
        // GET: Doc
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="leaf">是否显示页节点</param>
        /// <returns></returns>
        public ActionResult Tree(bool leaf=true)
        {
            var n = GetMenuTree(_docPath,leaf);

            return Json(n.nodes,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改目录
        /// </summary>
        /// <returns></returns>
        public ActionResult ModiDir(string parent, string sub)
        {
            var result = new JsonResultObj {IsSuccess = true};
            if (string.IsNullOrWhiteSpace(parent))
            {
                result.IsSuccess = false;
                result.Msg = "父目录为空";
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            parent = parent.Replace("/", "\\");
            if (parent.IndexOf("\\") == 0)
            {
                parent = parent.Substring(1);
            }
            var fullPath = Path.Combine(_docPath, parent);
            if (!Directory.Exists(fullPath))
            {
                result.IsSuccess = false;
                result.Msg = "目录不存在";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            //如果没传入子目录，则是删除
            if (string.IsNullOrWhiteSpace(sub))
            {
                if (string.IsNullOrWhiteSpace(parent))
                {
                    result.IsSuccess = false;
                    result.Msg = "根目录不能删除";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var files = Directory.GetFiles(fullPath);
                var dirs = Directory.GetDirectories(fullPath);
                if (files.Length > 0 || dirs.Length > 0)
                {
                    result.IsSuccess = false;
                    result.Msg = "目录下存在目录或者文件，不能删除";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                try
                {
                    Directory.Delete(fullPath);
                }
                catch (Exception e)
                {
                    result.IsSuccess = false;
                    result.Msg = "删除目录出错，错误信息：" + e.Message;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                result.IsSuccess = true;
                result.Msg = "删除成功";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            
            //添加目录
            var subPath = Path.Combine(fullPath, sub);
            if (Directory.Exists(subPath))
            {
                result.IsSuccess = true;
                result.Msg = "目录已经存在";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            try
            {
                Directory.CreateDirectory(subPath);
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Msg = "创建目录出错，错误信息：" + e.Message;
                return Json(result,JsonRequestBehavior.AllowGet);
            }

            result.IsSuccess = true;
            result.Msg = "添加成功";
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Up()
        {
            var result = new JsonResultObj();
            
            var postedFile = Request.Files["file"];
            if (postedFile == null)
            {
                result.IsSuccess = false;
                result.Msg = "无法获取到上传文件";
                return Json(result);
            }

            var path = Request.Form["path"];
            if (string.IsNullOrWhiteSpace(path))
            {
                result.IsSuccess = false;
                result.Msg = "请指定上传目录";
                return Json(result);
            }

            path = path.Replace("/", "\\");
            if (path.IndexOf("\\") == 0)
            {
                path = path.Substring(1);
            }
            var destDir = Path.Combine(_docPath, path);

            if (!Directory.Exists(destDir))
            {
                try
                {
                    Directory.CreateDirectory(destDir);
                }
                catch (Exception e)
                {
                    result.IsSuccess = false;
                    result.Msg = "创建目录出错，错误信息：" + e.Message;
                    return Json(result);
                }
            }

            var fullPath = Path.Combine(_docCachePath, postedFile.FileName);
            //缓存文件
            try
            {
                postedFile.SaveAs(fullPath);
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Msg = "缓存文件失败";
                return Json(result);
            }

            //将PDF转换成SWF
            ConvertMgr.AddToQueue(destDir,fullPath);

            result.IsSuccess = true;
            result.Msg = "缓存文件成功";
            return Json(result);
        }

        private TreeNode GetMenuTree(string dir, bool leaf)
        {
            if (!Directory.Exists(dir))
            {
                return null;
            }

            var dirName = dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar)+1);
            var dirPath = dir.Replace(_docPath, "").Replace("\\", "/");
            var tree = new TreeNode
                       {
                           text = dirName ,
                           nodes = new List<TreeNode>(),
                           state = new NodeState() { expanded = false},
                           path = dirPath,
                           selectable = !leaf
                       };

            var subDir = Directory.GetDirectories(dir);
            foreach (var d in subDir)
            {
                tree.nodes.Add(GetMenuTree(d,leaf));
            }
            //获取页节点
            if (leaf)
            {
                var files = Directory.GetFiles(dir);
                foreach (var f in files)
                {
                    var path = f.Replace(_docPath, "").Replace("\\", "/");
                    tree.nodes.Add(new TreeNode()
                    {
                        text = Path.GetFileNameWithoutExtension(f),
                        href = "javascript:changeDoc('" + path + "');",
                        path = path,
                        selectable = true
                    });
                }
            }
            

            return tree;
        }



    }
}