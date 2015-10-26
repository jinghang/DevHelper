using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADH.DevHelperWeb.Models;

namespace ADH.DevHelperWeb.Controllers
{
    public class DbController : Controller
    {
        SharemallEntities entities = new SharemallEntities();

        // GET: Db
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Left()
        {
            

            var row = entities.Database.SqlQuery < DbTable>(
                "select Name as TableName from sysobjects where xtype='u' and status >=0 and Name !='sysdiagrams' order by Name");
            return View(row.ToList());
        }

        public ActionResult RightContent(string TableName)
        {
            ViewBag.TableName = TableName;
            var sql = @"SELECT
                        [列名]=a.name,
                        [数据类型]=b.name,
                        [长度]=COLUMNPROPERTY(a.id,a.name,'PRECISION'),
                        [是否为空]=case when a.isnullable=1 then '√'else '' end,
                        [默认值]=isnull(e.text,''),
                        [说明]=isnull(g.[value],'未填说明')
                        FROM syscolumns a
                        left join systypes b on a.xusertype=b.xusertype
                        inner join sysobjects d on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
                        left join syscomments e on a.cdefault=e.id
                        left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id 
                        left join sys.extended_properties f on d.id=f.major_id and f.minor_id=0 where d.name='{0}' order by a.id,a.colorder";
            sql = string.Format(sql, TableName);
            var row = entities.Database.SqlQuery<DbTable>(sql);
            return View(row.ToList());
        }
    }
}