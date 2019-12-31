using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImgProjects.Models;

namespace ImgProjects.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        [HttpPost]
        public JsonResult PostFromUploadImg()//前台表单提交
        {
            var uploadPath = ConfigurationManager.AppSettings["UploadUrl"];//ip+端口 web.config
            uploadPath=Path.Combine(uploadPath);//转化为绝对路径
            var files = HttpContext.Request.Files;//.NET的去文件类型的属性
            List<Image> lists = new List<Image>();
            foreach (String item in files)//这取到的不一定 只有一个
            {
                var header = HttpContext.Request.Headers;
                var file = HttpContext.Request.Files[item];//取到指定的值
                if (file!=null)
                {
                    var fileExtension = Path.GetExtension(file.FileName);
                    var newFileName = Guid.NewGuid();
                    var newFile = string.Format("/yun/{0}{1}", newFileName, fileExtension);
                    var funPath = Path.Combine(uploadPath+newFile);
                    file.SaveAs(funPath);//file保存 在funPath 的路径下面
                    Image m = new Image {
                        Size = file.ContentLength,
                        ImageName = file.FileName,
                        ImageUrl = funPath,
                        Type="one"
                    };
                    lists.Add(m);
                }
                
            }
            return Json(lists);
        }
        [HttpPost]
        public JsonResult PostUploadImg()//post不属于from 的提价
        {
            var uploadPath = ConfigurationManager.AppSettings["UploadUrl"];//ip+端口 web.config
            uploadPath = Path.Combine(uploadPath);//转化为绝对路径
            var files = HttpContext.Request.Files;//.NET的去文件类型的属性
            List<Image> lists = new List<Image>();
            foreach (String item in files)//这取到的不一定 只有一个
            {
                var header = HttpContext.Request.Headers;
                var file = HttpContext.Request.Files[item];//取到指定的值
                if (file != null)
                {
                    var fileExtension = Path.GetExtension(file.FileName);
                    var newFileName = Guid.NewGuid();
                    var newFile = string.Format("/yun/{0}{1}", newFileName, fileExtension);//这里因为 没有类别瞎起 的名字(yun)
                    var funPath = Path.Combine(uploadPath + newFile);
                    file.SaveAs(funPath);//file保存 在funPath 的路径下面
                    Image m = new Image
                    {
                        Size = file.ContentLength,
                        ImageName = file.FileName,
                        ImageUrl = funPath,
                        Type = "one"
                    };
                    lists.Add(m);
                }

            }
            return Json(lists);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">path 图片存在的路径</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PostAllData(string path)
        {
            var uploadPath = ConfigurationManager.AppSettings["uploadImg"];//ip+端口 web.config
            string paths = "E://"+ path;
            DirectoryInfo root = new DirectoryInfo(paths);
            List<String> str = new List<string>();
            foreach (var item in root.GetFiles())
            {
                str.Add(uploadPath + "/yun/"+item.Name);
            }
            return Json(new {list=str });
        }
        [HttpPost]
        public JsonResult PostTest()//Post 测试连接
        {
            return Json(new { msg="post测试连接!"});
        }
        public JsonResult GetTest()//Get 测试连接
        {
            return Json(new {msg="Get测试连接" }, JsonRequestBehavior.AllowGet);
        }
    }
}
