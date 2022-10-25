using DotSpatial.Data;
using Exportable.Engines;
using Exportable.Engines.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaQNIN1.Models;
using TaQNIN1.Viewmodel;
using PagedList;
using ClosedXML.Excel;
using System.Data.OleDb;
using EGIS.ShapeFileLib;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using System.Data;
using System.Data.Entity.Spatial;
using DotSpatial.Topology;

namespace TaQNIN1.Controllers
{
    using GeoAPI.Geometries;
    using NetTopologySuite.Features;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.IO;
    using System.Text;
    using System.Globalization;
    using System.Web.UI.WebControls;
    using System.Web.UI;
    using System.Diagnostics;
    using System.ComponentModel;
    using OfficeOpenXml;
    using System.Data.SqlClient;
    public class userhomeController : Controller
    {
        enum MyEnum
        {
            id, name
        };
        //
        // GET: /userhome/
        ApplicationDbContext db = new ApplicationDbContext();

        //public ActionResult doownload()
        //{

        //    return View();
        //}
        public FileResult ExportListUsingEPPlus()
        {

            //func();
            ApplicationDbContext db = new ApplicationDbContext();

            System.Data.DataTable dt = new System.Data.DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[19] { new DataColumn("رقم الطلب"),  
                                                     new DataColumn("امر الشغل"),
                                                       new DataColumn("المحافظه"),
                                                       new DataColumn("حاله الطلب"),
                                                        new DataColumn("اسم مقدم الطلب"),
                                                         new DataColumn("نوع النشاط"),
                                                         new DataColumn("تاريخ الطلب"),
                                                         new DataColumn("اسم المستخدم لطلب اعاده الدراسه"),
                                                          new DataColumn("المركز الجغرافي"),
                                                          new DataColumn("قرار مركز المتغيرات"),  new DataColumn("القانونيه"), new DataColumn("ملاحظات الدراسه"), new DataColumn("المساحه"), new DataColumn("وحده القياس"),
                                                           new DataColumn("التظلم")
                                                     ,new DataColumn("المساحه الواقعيه"),
                                                           new DataColumn("الرد"),
                                                           new DataColumn("تاريخ الرد"),new DataColumn("حاله الرد")
                                                            
                                                    });
              var lst = searchexcel(null, null, null, null, null, null, null, null, null, null,
                   null, null, null, null, null, null, null, null);





              foreach (var item in lst)
              {
                 var legal = item.LegalFullfied == null ? "" : item.LegalFullfied.Trim();
                  var gov = item.governate == null ? "" : item.governate.Trim();
                  var stat = item.status == null ? "" : item.status.Trim();
                  var nam = item.name == null ? "" : item.name.Trim();
                  var activit = item.activity == null ? "" : item.activity.Trim();
                  var uploaddat = item.uploaddate == null ? "" : item.uploaddate.Trim();
                  var studentUse = item.studentUser == null ? "" : item.studentUser.Trim();
                  var geographic_person_respons = item.geographic_person_response == null ? "" : item.geographic_person_response.Trim();
                  var ChangesCenterDescion = item.ChangesCenterDescion == null ? "" : item.ChangesCenterDescion.Trim();
                  var studynote = item.studynotes == null ? "" : item.studynotes.Trim();
                  var unit1 = item.unit == null ? "" : item.unit.Trim();
                  var tazalom1 = item.tazalom == null ? "" : item.tazalom.Trim();

                  var respdate = item.responsedate == null ? "" : item.responsedate.Trim();
                  var responseuser = item.responseUser == null ? "" : item.responseUser.Trim();
                  var responsestatus = item.responsestatus == null ? "" : item.responsestatus.Trim();
                  dt.Rows.Add(item.id_no, item.income_no.Trim(), gov, stat, nam, activit, uploaddat, studentUse,
                       geographic_person_respons, ChangesCenterDescion, legal, studynote, item.area, unit1, tazalom1, item.Convertedspace, respdate, responseuser, responsestatus);

              }

              using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
              {
                  //wb.ShowRowColHeaders ;
                  wb.RowHeight = 10;
                  wb.Worksheets.Add(dt);
                  using (MemoryStream stream = new MemoryStream()) //using System.IO;  
                  {
                      wb.SaveAs(stream);
                      return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFile.xlsx");
                  }


              }
            //var gv = new GridView();




            //var query = from taqnin in db.TaqninData
            //            from taqninmeta in db.TaqninMetadata
            //            where taqnin.Taqninid == taqninmeta.Taqninid && (taqninmeta.OrderStatus == "تم الرد والحفظ فى الارشيف" || taqninmeta.ResponseApproval == "موافق")
            //            select new systemdata
            //            {
            //                id_no = taqnin.id_no
            //                ,
            //                Taqninid = taqnin.Taqninid
            //                ,
            //                LegalFullfied = taqninmeta.LegalFullfied,
            //                ChangesCenterDescion = taqninmeta.ChangesCenterDescion,
            //                id = taqninmeta.id,
            //                w_man = taqnin.w_man
            //                ,
            //                studentUser = taqninmeta.studentUser,
            //                geographic_person_response = taqninmeta.geographic_person_response,
            //                activity = taqnin.activity,
            //                governate = taqnin.governate,
            //                actualarea = taqnin.actualarea,
            //                income_no = taqnin.income_no,
            //                tazalom = taqnin.tazalom,
            //                area = taqnin.area,
            //                shapearea = taqnin.shapearea,
            //                status = taqnin.status,
            //                study_note = taqninmeta.studynotes,
            //                name = taqnin.name,
            //                unit = taqnin.unit,
            //                uploaddate = taqninmeta.uploaddate,
            //                responseUser = taqninmeta.responseUser,
            //                responsedate = taqninmeta.responsedate,
            //                statuss = taqninmeta.responsestatus
            //            };
            //var lst = query.ToList();
            //gv.DataSource = lst;
            //gv.DataBind();
            //Response.ClearContent();
            //Response.Buffer = true;
            //gv.Height = 1;
            //Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.csv");
            //Response.ContentType = "application/CSV";
            //Response.Charset = "";
            //StringWriter objStringWriter = new StringWriter();
            //HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            //gv.RenderControl(objHtmlTextWriter);
            //Response.Output.Write(objStringWriter.ToString());
            //Response.Flush();
            //Response.End();
        }


        public void ExportListUsingEPPlus2()
        {

            //func();
            ApplicationDbContext db = new ApplicationDbContext();
            var gv = new GridView();

           


            var query = from taqnin in db.TaqninData
                        from taqninmeta in db.TaqninMetadata
                        where taqnin.Taqninid == taqninmeta.Taqninid
                        select new systemdata
                        {
                            id_no = taqnin.id_no
                            ,
                            Taqninid = taqnin.Taqninid
                            ,
                            LegalFullfied = taqninmeta.LegalFullfied,
                            ChangesCenterDescion = taqninmeta.ChangesCenterDescion,
                            id = taqninmeta.id,
                            w_man = taqnin.w_man
                            ,
                            studentUser = taqninmeta.studentUser,
                            geographic_person_response = taqninmeta.geographic_person_response,
                            activity = taqnin.activity,
                            governate = taqnin.governate,
                            actualarea = taqnin.actualarea,
                            income_no = taqnin.income_no,
                            tazalom = taqnin.tazalom,
                            area = taqnin.area,
                            shapearea = taqnin.shapearea,
                            status = taqnin.status,
                            study_note = taqninmeta.studynotes,
                            name = taqnin.name,
                            unit = taqnin.unit,
                            uploaddate = taqninmeta.uploaddate,
                            responseUser = taqninmeta.responseUser,
                            responsedate = taqninmeta.responsedate,
                            statuss = taqninmeta.responsestatus
                        };
            var lst = query.ToList();
            gv.DataSource = lst;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            gv.Height = 1;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Officer()
        {

            var user = db.Users.SingleOrDefault(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name);
            if (User.Identity.IsAuthenticated && user.userrole == "صف الظابط")
                return View();
            else return RedirectToAction("Login", "Account");
        }
        public ActionResult admin(string role)
        {
            //updatemdbdata();
            var data = db.TaqninMetadata.ToList();
            //getdata(data);
            ViewBag.userrole = role;
            var user = db.Users.SingleOrDefault(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name);
            if (User.Identity.IsAuthenticated && user.userrole == "ادمن")
                return View();
            else return RedirectToAction("Login", "Account");

        }

        public ActionResult student()
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name);
            if (User.Identity.IsAuthenticated && user.userrole == "دارس")

                return View();
            else return RedirectToAction("Login", "Account");
        }
        public ActionResult responseuser()
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name);
            if (User.Identity.IsAuthenticated && user.userrole == "الرد")
                return View();
            else return RedirectToAction("Login", "Account");
        }
        public ActionResult StudyAndResponse()
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name);
            if (User.Identity.IsAuthenticated && user.userrole == "دراسه ورد")

                return View();
            else return RedirectToAction("Login", "Account");
        }
        public ActionResult majorUser()
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name);
            if (User.Identity.IsAuthenticated && user.userrole == "لواء")
                return View();
            else return RedirectToAction("Login", "Account");
        }

        public ActionResult captain()
        {
            if (User.Identity.IsAuthenticated)
                return View();
            else return RedirectToAction("Login", "Account");
        }
        public ActionResult PoineerUser()

        {
          
            var user = db.Users.SingleOrDefault(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name);
            if (User.Identity.IsAuthenticated && user.userrole == "قائد المركز")
                return View();
            else return RedirectToAction("Login", "Account");
        }


        [HttpGet]
        public ActionResult ShapefileUpload()
        {
            if (User.Identity.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult ShapefileUpload(List<HttpPostedFileBase> uploadshape)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name);
            if (uploadshape.Count != 0)
            {


                string Folderpath = Server.MapPath("~/Uploads");
                var Shapepepath = "";
                for (int i = 0; i < uploadshape.Count; i++)
                {

                    string path = Path.Combine(Server.MapPath("~/Uploads"), uploadshape[i].FileName);
                    if (uploadshape[i].FileName.Contains("shp") && !uploadshape[i].FileName.Contains("xml"))
                        Shapepepath = path;
                    uploadshape[i].SaveAs(path);

                }

                var shapes = DotSpatial.Data.Shapefile.OpenFile(Shapepepath);
                var taqnin = new TaqninData();
                var shape = shapes.GetShape(0, true);
                var shapecheck = shapes.GetShape(0, true);
                string incom = shapecheck.Attributes[10].ToString();
                var idno = shapecheck.Attributes[1].ToString();
                var checkExist = db.TaqninData.Where(x => x.income_no == incom && x.id_no == idno).ToList();
                if (String.IsNullOrEmpty(incom))
                {
                    ViewBag.ErrorMessage = "لا يمكن الرفع";
                    DeleteFiles(Folderpath);
                    return View();
                }
                if (checkExist.Count == 0)
                {




                    for (int x = 0; x < shapes.DataTable.Rows.Count; x++)
                    {
                        taqnin = new TaqninData();
                        shape = shapes.GetShape(x, true);
                        taqnin.id_no = shape.Attributes[1].ToString();
                        taqnin.name = shape.Attributes[2].ToString();
                        taqnin.activity = shape.Attributes[3].ToString();
                        taqnin.governate = shape.Attributes[4].ToString();
                        taqnin.unit = shape.Attributes[5].ToString();
                        taqnin.area = double.Parse(shape.Attributes[6].ToString());
                        taqnin.w_man = shape.Attributes[7].ToString();
                        taqnin.tazalom = shape.Attributes[8].ToString() == "" ? "لايوجد" : shape.Attributes[8].ToString();
                        taqnin.study_note = shape.Attributes[9].ToString();
                        taqnin.income_no = shape.Attributes[10].ToString();
                        taqnin.status = shape.Attributes[11].ToString();
                        taqnin.shapelength = double.Parse(shape.Attributes[13].ToString());
                        taqnin.shapearea = double.Parse(shape.Attributes[14].ToString());
                        taqnin.actualarea = 0;
                        var zz = (shapes.Features[x].BasicGeometry).ToString();
                        taqnin.PolygonData = (DbGeography)DbGeography.FromText(zz);
                        taqnin.Commission = shape.Attributes[15].ToString();
                        taqnin.Place = shape.Attributes[16].ToString();
                        taqnin.Sheikhah = shape.Attributes[17].ToString();
                        taqnin.center = shape.Attributes[18].ToString();
                        taqnin.CreatedBY = System.Web.HttpContext.Current.User.Identity.Name;
                        taqnin.CreatedTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                        taqnin.CreatedDevice = DetermineCompName();
                        taqnin.Updated = System.Web.HttpContext.Current.User.Identity.Name;
                        taqnin.UpdatedDevice = DetermineCompName();
                        taqnin.UpdatedTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");

                        
                        db.TaqninData.Add(taqnin);






                        var taqninMeta = new TaqninMetadata();
                        taqninMeta.Taqninid = taqnin.Taqninid;
                        
                        taqninMeta.uploaddate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                        //taqninMeta.majorUser = "";
                        taqninMeta.person_upload = System.Web.HttpContext.Current.User.Identity.Name;
                        taqninMeta.ReviewFlag = false;
                        taqninMeta.SuspendedOrder = false;
                        taqninMeta.ImageAfterCounter = 0;
                        taqninMeta.ImageBeforeCounter = 0;
                        taqninMeta.responsestatus = "not response";
                        taqninMeta.OrderStatus = "دراسه المركز الجغرافي";
                        taqninMeta.Convertedspace = Math.Round(ConvertArea(double.Parse(shape.Attributes[14].ToString()), shape.Attributes[5].ToString()), 3);
                        db.TaqninMetadata.Add(taqninMeta);
                        var log = new LogTable();
                        log.userName = System.Web.HttpContext.Current.User.Identity.Name;
                        log.id_no = taqnin.id_no;
                        log.action = "shape fileرفع ال ";
                        log.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                        db.LogTable.Add(log);
                        db.SaveChanges();
                    }
                    var income = new Income_noData();
                    income.income_no = incom;
                    income.uploaddate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                    income.ordersCount = shapes.DataTable.Rows.Count;
                    var rr = db.TaqninData.ToList().Count;
                    income.insideOrdersCount = 0;
                    income.outsideOrdersCount = 0;
                    income.geographicperson = "";
                    db.Income_noData.Add(income);
                    db.SaveChanges();
                    DeleteFiles(Folderpath);
                }
                else
                {
                    var data = db.TaqninData.First();
                    var x = data.PolygonData;
                    DeleteFiles(Folderpath);
                    ViewBag.ErrorMessage = "تم الرفع من قبل";
                    return View();
                }

            }
           
            AccountController account = new AccountController();
            return account.DefineUser(user);
        }

        [HttpGet]
        public ActionResult pointsfileUpload()
        {
            if (User.Identity.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        public ActionResult pointsfileUpload(List<HttpPostedFileBase> uploadspoints)
        {
            var PointsShapepepath = "";
            if (uploadspoints.Count != 0)
            {


                string poinsFolderpath = Server.MapPath("~/PointsUpload");

                for (int i = 0; i < uploadspoints.Count; i++)
                {

                    string path = Path.Combine(Server.MapPath("~/PointsUpload"), uploadspoints[i].FileName);
                    if (uploadspoints[i].FileName.Contains("shp") && !uploadspoints[i].FileName.Contains("xml"))
                        PointsShapepepath = path;
                    uploadspoints[i].SaveAs(path);

                }
                if (PointsShapepepath != "")
                {
                    var Pointss = DotSpatial.Data.Shapefile.OpenFile(PointsShapepepath);
                    for (int p = 0; p < Pointss.DataTable.Rows.Count; p++)
                    {
                        var model = new Points();
                        var shapepoint = Pointss.GetShape(p, true);

                        model.idno = shapepoint.Attributes[3].ToString();
                        model.incomeno = shapepoint.Attributes[12].ToString();
                        model.x = double.Parse(shapepoint.Attributes[1].ToString());
                        model.y = double.Parse(shapepoint.Attributes[2].ToString());
                        db.Points.Add(model);
                        db.SaveChanges();

                    }

                    return RedirectToAction("Officer");
                }

            }
            ViewBag.ErrorMessage = "تم الرفع من قبل";
            return View();
        }
        public double ConvertArea(double shapearea, string unit)
        {

            switch (unit)
            {
                case "م2":
                    return shapearea;

                case "فدان":
                    return (shapearea / 4200.83);

                case "قيراط":
                    return (shapearea / 175.02);

                case "سهم":
                    return (shapearea / 7.29);

            }

            return shapearea;
        }
        public void DeleteFiles(string path)
        {

            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

       
        public ActionResult Variables_center_decision(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom1,
            string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity, int? searchh, string revieweruser, string LegalFullfied, string ChangesCenterDescion1,int?page)
        {
           // var xxx = DetermineCompName();
            int pageindex = 1;
            int pagesize = 20;

            pageindex = page.HasValue ? Convert.ToInt32(page) : pageindex;

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.actioname = "Variables_center_decision";

                ViewBag.users = new SelectList(GetUsers().ToList());
                ViewBag.governamentlst = new SelectList(GetGovernament().ToList());
                ViewBag.incomelst = GetIncome().Select(r => new SelectListItem { Text = r, Value = r });
                ViewBag.Status = new SelectList(Status().ToList());
                var xx = GetIncome().Select(r => new SelectListItem { Text = r, Value = r });
                ViewBag.idnoselected = id_no;
                ViewBag.income_noselected = income_no;
                ViewBag.geographic_person_responseselected = geographic_person_response;
                ViewBag.governateselected = governate;
                ViewBag.studentUserselected = studentUser;
                ViewBag.Descion223selected = Descion223;
                ViewBag.nameselected = name;
                ViewBag.DescionQMselected = DescionQM;
                ViewBag.responsedateselected = responsedate;
                ViewBag.tazalomselected = tazalom1;
                ViewBag.RaiseSurveyorsselected = RaiseSurveyors;
                ViewBag.fullfiltermsselected = fullfilterms;
                ViewBag.statusselected = status;
                ViewBag.Delayedselected = Delayed;
                ViewBag.activityselected = activity;
                ViewBag.revieweruserselected = revieweruser;
                ViewBag.LegalFullfiedselected = LegalFullfied;
                ViewBag.ChangesCenterDescionselected = ChangesCenterDescion1;

                     var lst = searchbystudy(id_no, income_no, geographic_person_response, governate, studentUser, Descion223, name, DescionQM, responsedate, tazalom1,
                    RaiseSurveyors, fullfilterms, status, Delayed, activity, revieweruser, LegalFullfied, ChangesCenterDescion1);
                    ViewBag.count = lst.Count;
                    return View(lst.ToPagedList(pageindex, pagesize));
                //if (searchh == 1)
                //{
                //    Session["studysearch"] = 1;

                //    Session["studyincome_noselected"] = income_no;
                //    Session["studygeographic_person_responseselected"] = geographic_person_response;
                //    Session["studygovernateselected"] = governate;
                //    Session["studystudentUserselected"] = studentUser;

                //    Session["studytazalomselected"] = tazalom;

                //    Session["studyfullfiltermsselected"] = fullfilterms;
                //    Session["studystatusselected"] = status;

                //    Session["studyactivityselected"] = activity;

                //    Session["studyLegalFullfiedselected"] = LegalFullfied;
                //    Session["studyChangesCenterDescionselected"] = ChangesCenterDescion;

                //    //searchh = 0;
                //    var lst = searchby1(id_no, income_no, geographic_person_response, governate, studentUser, Descion223, name, DescionQM, responsedate, tazalom,
                //     RaiseSurveyors, fullfilterms, status, Delayed, activity, revieweruser, LegalFullfied, ChangesCenterDescion);
                //    var lst1 = lst.Where(m => (!String.IsNullOrEmpty(m.geographic_person) && (String.IsNullOrEmpty(m.studentUser))) || ((m.BackToVariableCenter == 1) && !String.IsNullOrEmpty(m.studentUser)) && m.SuspendedOrder == false && m.responsestatus != "response" && m.OrderStatus != "تم الرد والحفظ فى الارشيف");
                   
                            
                //    List<systemdata> system = new List<systemdata>();
                //    foreach (var x in lst1)
                //    {
                //        var item = new systemdata
                //        {
                //            id_no = x.TaqninData.id_no
                //            ,
                //            Taqninid = x.Taqninid
                //            ,
                //            LegalFullfied = x.LegalFullfied,
                //            ChangesCenterDescion = x.ChangesCenterDescion,
                //            id = x.id,
                //            w_man = x.TaqninData.w_man
                //            ,
                //            studentUser = x.studentUser,
                //            geographic_person_response = x.geographic_person_response,
                //            activity = x.TaqninData.activity,
                //            governate = x.TaqninData.governate,
                //            actualarea = x.TaqninData.actualarea,
                //            income_no = x.TaqninData.income_no,
                //            tazalom = x.TaqninData.tazalom,
                //            area = x.TaqninData.area,
                //            shapearea = x.TaqninData.shapearea,
                //            status = x.TaqninData.status,
                //            study_note = x.TaqninData.study_note,
                //            name = x.TaqninData.name,
                //            unit = x.TaqninData.unit,
                //            uploaddate = x.uploaddate,
                //            responsedate = x.responsedate

                //        };
                //        system.Add(item);
                //    }
                //    ViewBag.count = system.Count();

                //    return View(system);
                //}

                //else if (Session["studysearch"] != null)
                //{

                //    if (Session["studysearch"].ToString() == "1" && searchh != 0)
                //    {

                       
                //        income_no = Session["studyincome_noselected"].ToString() == "None" ? "" : Session["studyincome_noselected"].ToString();
                //        geographic_person_response = Session["studygeographic_person_responseselected"].ToString() == "None" ? "" : Session["studygeographic_person_responseselected"].ToString();
                //        governate = Session["studygovernateselected"].ToString() == "None" ? "" : Session["studygovernateselected"].ToString();
                //        studentUser = Session["studystudentUserselected"].ToString() == "None" ? "" : Session["studystudentUserselected"].ToString();
                //        tazalom = Session["studytazalomselected"].ToString() == "None" ? "" : Session["studytazalomselected"].ToString();
                //         fullfilterms = Session["studyfullfiltermsselected"].ToString() == "None" ? "" : Session["studyfullfiltermsselected"].ToString();
                //        status = Session["studystatusselected"].ToString() == "None" ? "" : Session["studystatusselected"].ToString();
                //         activity = Session["studyactivityselected"] == null ? "" : Session["studyactivityselected"].ToString();
                //          LegalFullfied = Session["studyLegalFullfiedselected"] == null ? "" : Session["studyLegalFullfiedselected"].ToString();
                //        ChangesCenterDescion = Session["studyChangesCenterDescionselected"] == null ? "" : Session["studyChangesCenterDescionselected"].ToString();

                //        ViewBag.idnoselected = id_no;
                //        ViewBag.income_noselected = income_no == "" ? "None" : income_no;
                //        ViewBag.geographic_person_responseselected = geographic_person_response == "" ? "None" : geographic_person_response;
                //        ViewBag.governateselected = governate == "" ? "None" : governate;
                //        ViewBag.studentUserselected = studentUser == "" ? "None" : studentUser;
                //        ViewBag.Descion223selected = Descion223 == "" ? "None" : Descion223;
                //        ViewBag.nameselected = name;
                //        ViewBag.DescionQMselected = DescionQM == "" ? "None" : DescionQM;
                //        ViewBag.responsedateselected = responsedate;
                //        ViewBag.tazalomselected = tazalom == "" ? "None" : tazalom;
                //        ViewBag.RaiseSurveyorsselected = RaiseSurveyors == "" ? "None" : RaiseSurveyors;
                //        ViewBag.fullfiltermsselected = fullfilterms == "" ? "None" : fullfilterms;
                //        ViewBag.statusselected = status == "" ? "None" : status;
                //        ViewBag.Delayedselected = Delayed == "" ? "None" : Delayed;
                //        ViewBag.activityselected = activity == "" ? "None" : activity;
                //        ViewBag.revieweruserselected = revieweruser == "" ? "None" : revieweruser;
                //        ViewBag.LegalFullfiedselected = LegalFullfied == "" ? "None" : LegalFullfied;
                //        ViewBag.ChangesCenterDescionselected = ChangesCenterDescion == "" ? "None" : ChangesCenterDescion;
                //        //searchh = 0;
                //        var lst = searchby(id_no, income_no, geographic_person_response, governate, studentUser, Descion223, name, DescionQM, responsedate, tazalom,
                //         RaiseSurveyors, fullfilterms, status, Delayed, activity, revieweruser, LegalFullfied, ChangesCenterDescion);
                //        var lst1 = lst.Where(m => (!String.IsNullOrEmpty(m.geographic_person) && (String.IsNullOrEmpty(m.studentUser))) || ((m.BackToVariableCenter == 1) && !String.IsNullOrEmpty(m.studentUser)) && m.SuspendedOrder == false && m.OrderStatus != "تم الرد والحفظ فى الارشيف");
                //        List<systemdata> system = new List<systemdata>();
                //        foreach (var x in lst1)
                //        {
                //            var item = new systemdata
                //            {
                //                id_no = x.TaqninData.id_no
                //                ,
                //                Taqninid = x.Taqninid
                //                ,
                //                LegalFullfied = x.LegalFullfied,
                //                ChangesCenterDescion = x.ChangesCenterDescion,
                //                id = x.id,
                //                w_man = x.TaqninData.w_man
                //                ,
                //                studentUser = x.studentUser,
                //                geographic_person_response = x.geographic_person_response,
                //                activity = x.TaqninData.activity,
                //                governate = x.TaqninData.governate,
                //                actualarea = x.TaqninData.actualarea,
                //                income_no = x.TaqninData.income_no,
                //                tazalom = x.TaqninData.tazalom,
                //                area = x.TaqninData.area,
                //                shapearea = x.TaqninData.shapearea,
                //                status = x.TaqninData.status,
                //                study_note = x.TaqninData.study_note,
                //                name = x.TaqninData.name,
                //                unit = x.TaqninData.unit,
                //                uploaddate = x.uploaddate,
                //                responsedate = x.responsedate

                //            };
                //            system.Add(item);
                //        }
                //        ViewBag.count = system.Count();

                //        return View(system);
                //    }
                //    Session["studysearch"] = null;
                    
                //    Session["studyincome_noselected"] = null;
                //    Session["studygeographic_person_responseselected"] = null;
                //    Session["studygovernateselected"] = null;
                //    Session["studystudentUserselected"] = null;
                   
                //    Session["studynameselected"] = null;
                   
                //    Session["studytazalomselected"] = null;
                    
                //    Session["studystatusselected"] = null;
             
                //    Session["studyactivityselected"] = null;
    
                //    Session["studyLegalFullfiedselected"] = null;
                //    Session["studyChangesCenterDescionselected"] = null;



                //    var query = from taqnin in db.TaqninData
                //                from taqninmeta in db.TaqninMetadata
                //                where taqnin.Taqninid == taqninmeta.Taqninid && ((!String.IsNullOrEmpty(taqninmeta.geographic_person) && (String.IsNullOrEmpty(taqninmeta.studentUser))) || ((taqninmeta.BackToVariableCenter == 1) && !String.IsNullOrEmpty(taqninmeta.studentUser)) && taqninmeta.SuspendedOrder == false)
                //                select new systemdata
                //                {
                //                    id_no = taqnin.id_no
                //                    ,
                //                    Taqninid = taqnin.Taqninid
                //                    ,
                //                    LegalFullfied = taqninmeta.LegalFullfied,
                //                    ChangesCenterDescion = taqninmeta.ChangesCenterDescion,
                //                    id = taqninmeta.id,
                //                    w_man = taqnin.w_man
                //                    ,
                //                    studentUser = taqninmeta.studentUser,
                //                    geographic_person_response = taqninmeta.geographic_person_response,
                //                    activity = taqnin.activity,
                //                    governate = taqnin.governate,
                //                    actualarea = taqnin.actualarea,
                //                    income_no = taqnin.income_no,
                //                    tazalom = taqnin.tazalom,
                //                    area = taqnin.area,
                //                    shapearea = taqnin.shapearea,
                //                    status = taqnin.status,
                //                    study_note = taqnin.study_note,
                //                    name = taqnin.name,
                //                    unit = taqnin.unit,
                //                    uploaddate = taqninmeta.uploaddate,
                //                    responsedate = taqninmeta.responsedate
                //                };
                //    ViewBag.count = query.ToList().Count();
                  
                //    return View(query.ToList());
                //}


                //else
                //{
                //    Session["studysearch"] = null;
                    
                //    Session["studyincome_noselected"] = null;
                //    Session["studygeographic_person_responseselected"] = null;
                //    Session["studygovernateselected"] = null;
                //    Session["studystudentUserselected"] = null;
                   
                //    Session["studytazalomselected"] = null;
                    
                //    Session["studyfullfiltermsselected"] = null;
                //    Session["studystatusselected"] = null;
                   
                //    Session["studyactivityselected"] = null;
                    
                //    Session["studyLegalFullfiedselected"] = null;
                //    Session["studyChangesCenterDescionselected"] = null;



                //    var query = from taqnin in db.TaqninData
                //                from taqninmeta in db.TaqninMetadata
                //                where taqnin.Taqninid == taqninmeta.Taqninid && ((!String.IsNullOrEmpty(taqninmeta.geographic_person) && (String.IsNullOrEmpty(taqninmeta.studentUser))) || ((taqninmeta.BackToVariableCenter == 1) && !String.IsNullOrEmpty(taqninmeta.studentUser)) && taqninmeta.SuspendedOrder == false && (taqninmeta.responsestatus !="response"&& taqninmeta.OrderStatus != "تم الرد والحفظ فى الارشيف"))
                //                select new systemdata
                //                {
                //                    id_no = taqnin.id_no
                //                    ,
                //                    Taqninid = taqnin.Taqninid
                //                    ,
                //                    LegalFullfied = taqninmeta.LegalFullfied,
                //                    ChangesCenterDescion = taqninmeta.ChangesCenterDescion,
                //                    id = taqninmeta.id,
                //                    w_man = taqnin.w_man,
                //                    studentUser = taqninmeta.studentUser,
                //                    geographic_person_response = taqninmeta.geographic_person_response,
                //                    activity = taqnin.activity,
                //                    governate = taqnin.governate,
                //                    actualarea = taqnin.actualarea,
                //                    income_no = taqnin.income_no,
                //                    tazalom = taqnin.tazalom,
                //                    area = taqnin.area,
                //                    shapearea = taqnin.shapearea,
                //                    status = taqnin.status,
                //                    study_note = taqnin.study_note,
                //                    name = taqnin.name,
                //                    unit = taqnin.unit,
                //                    uploaddate = taqninmeta.uploaddate,
                //                    responsedate = taqninmeta.responsedate
                //                };
                //    ViewBag.count = query.ToList().Count();

                //    return View(query.ToList());



                }
            

            else
                return RedirectToAction("Login", "Account");


        }

        public ActionResult studyData(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var taqninmeta = db.TaqninMetadata.FirstOrDefault(x => x.id == id);
                var taqninmeta_gpr = taqninmeta.geographic_person_response;
                var xxx = taqninmeta.TaqninData.actualarea;
                List<string> tt = new List<string>
            {
                "خارج", "داخل"
            };

                ViewBag.Geo_person_response = new SelectList(tt, taqninmeta_gpr);
                if (taqninmeta.PreviewDate == null || taqninmeta.PreviewDate == "")
                    taqninmeta.PreviewDate = DateTime.Now.ToString("dd/MM/yyyy");
                if (taqninmeta.Imagedate == null || taqninmeta.Imagedate == "")
                    taqninmeta.Imagedate = DateTime.Now.ToString("dd/MM/yyyy");
                return View(taqninmeta);
            }
            else
                return RedirectToAction("Login", "Account");
        }

        public ActionResult savedata(TaqninMetadata taqninmeta, FormCollection frm)
        {

            var taqninmetaa = db.TaqninMetadata.FirstOrDefault(x => x.id == taqninmeta.id);

            taqninmeta.geographic_person_response = frm["Geo_person_response"];
            taqninmeta.AcceptOrNot = frm["AcceptOrNot"];
            var incomedata = db.Income_noData.FirstOrDefault(y => y.income_no == taqninmetaa.TaqninData.income_no);
            if (taqninmeta.geographic_person_response == "داخل")
            {
                if (incomedata.insideOrdersCount <= 0 && taqninmetaa.geographic_person_response == "داخل") incomedata.insideOrdersCount = 1;
                taqninmetaa.DescionQM = frm["DescionQM"];
                taqninmetaa.Descion223 = frm[" Descion223"];
                taqninmetaa.RaiseSurveyors = frm["RaiseSurveyors"];
                taqninmetaa.Overlap_after_range = frm["Overlap_after_range"];

            }
            if (taqninmetaa.geographic_person_response == "داخل" && taqninmeta.geographic_person_response == "خارج")
            {
               incomedata.insideOrdersCount -= 1;
                if (incomedata.insideOrdersCount < 0) { incomedata.insideOrdersCount = 0; }
                incomedata.outsideOrdersCount += 1;
                taqninmetaa.Overlap_after_range = "";
                taqninmeta.RaiseSurveyors = "";
                taqninmeta.Descion223 = "";
                taqninmeta.DescionQM = "";

            }
            else if (taqninmeta.geographic_person_response == "داخل" && taqninmetaa.geographic_person_response == "خارج")
            {

                incomedata.outsideOrdersCount -= 1;
                incomedata.insideOrdersCount += 1;

            }
            if (incomedata.insideOrdersCount < 0) { incomedata.insideOrdersCount = 0; }
            if (incomedata.outsideOrdersCount < 0) { incomedata.outsideOrdersCount = 0; }

            taqninmetaa.geographic_person_response = taqninmeta.geographic_person_response;
            taqninmetaa.Remainingspace = taqninmeta.Remainingspace;
            taqninmetaa.AcceptOrNot = taqninmeta.AcceptOrNot;
            var log = new LogTable();
            log.userName = System.Web.HttpContext.Current.User.Identity.Name;
            log.id_no = taqninmetaa.TaqninData.id_no;
            log.action = "دراسه المركز الجغرافي";
            log.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            db.LogTable.Add(log);
            db.SaveChanges();
            return RedirectToAction("GeographicCenterOrders", new { id = taqninmetaa.TaqninData.income_no });
        }

        public ActionResult geographiccenter()
        {
            if (User.Identity.IsAuthenticated)
            {

                var items = db.Income_noData.Where(x => x.geographicperson == "" || (x.PoineerApproval == "ارجاع")).ToList();

                return View(items);
            }

            else
                return RedirectToAction("Login", "Account");
        }
        public ActionResult geographiccenteruser()
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name);
            if (User.Identity.IsAuthenticated && user.userrole == "المركز الجغرافى")
            {

                var items = db.Income_noData.Where(x => x.geographicperson == "" || x.PoineerApproval == "ارجاع").ToList();

                return View();
            }

            else
                return RedirectToAction("Login", "Account");


        }
        public ActionResult geographiccenteroverlabs()
        {

            if (User.Identity.IsAuthenticated)
            {
                var lst = db.TaqninMetadata.Where(x => ((String.IsNullOrEmpty(x.geographic_person_response) && x.SuspendedOrder == false) || x.BackToGeographic == 1) && x.TaqninData.income_no != "").Select(i => i.TaqninData.income_no).Distinct().ToList();
                ViewBag.income = new SelectList(lst);
                return View();
            }
            else
                return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult overlabs(string type, string income, List<HttpPostedFileBase> uploadshape)
        {
            if (User.Identity.IsAuthenticated)
            {
                Session["enterd"] = "true";

                var incomedata = db.Income_noData.FirstOrDefault(x => x.income_no == income);
                var lst = db.TaqninMetadata.Where(x => x.TaqninData.income_no == income).ToList();
                if (lst.Any(x => String.IsNullOrEmpty(x.geographic_person)))
                {
                    lst.ForEach(a =>
                    {
                        a.AcceptOrNot = "موافق";
                        a.geographic_person_response = type;
                    }
                        );

                    if (type == "داخل")
                    {
                        string idno = "";
                        string incomeno = "";

                        incomedata.insideOrdersCount = incomedata.ordersCount;
                        string Folderpath = Server.MapPath("~/Uploads");
                        var Shapepepath = "";
                        for (int i = 0; i < uploadshape.Count; i++)
                        {

                            string path = Path.Combine(Server.MapPath("~/Uploads"), uploadshape[i].FileName);
                            if (uploadshape[i].FileName.Contains("shp") && !uploadshape[i].FileName.Contains("xml"))
                                Shapepepath = path;
                            uploadshape[i].SaveAs(path);

                        }
                        var shapes = DotSpatial.Data.Shapefile.OpenFile(Shapepepath);

                        for (int x = 0; x < shapes.DataTable.Rows.Count - 1; x++)
                        {
                            var shape = shapes.GetShape(x, true);
                            idno = shape.Attributes[0].ToString();
                            incomeno = shape.Attributes[1].ToString();

                            var incomee = db.TaqninMetadata.Where(z => z.TaqninData.income_no == incomeno && z.TaqninData.id_no == idno).ToList()[0];
                            incomee.RaiseSurveyors = shape.Attributes[3].ToString();
                            incomee.Descion223 = shape.Attributes[2].ToString();
                            incomee.DescionQM = shape.Attributes[4].ToString();
                            
                            db.SaveChanges();

                        }
                        DeleteFiles(Folderpath);
                    }
                    else
                        incomedata.outsideOrdersCount = incomedata.ordersCount;
                }
                else
                {
                    var lstback = lst.Where(x => x.BackToGeographic == 1 && x.ResponseApproval != "موافق").ToList();
                    lstback.ForEach(a =>
                    {
                        a.AcceptOrNot = "موافق";
                        a.geographic_person_response = type;
                    }
                      );

                    if (type == "داخل")
                        incomedata.insideOrdersCount = lstback.Count();
                    else
                        incomedata.outsideOrdersCount = lstback.Count();
                }

                db.SaveChanges();

                return RedirectToAction("geographiccenteruser");
            }
            else
                return RedirectToAction("Login", "Account");
        }


        public ActionResult GeographicCenterOrders(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Session["enterd"] = "true";
                var query = from taqnin in db.TaqninData
                            from taqninmeta in db.TaqninMetadata
                            where taqnin.Taqninid == taqninmeta.Taqninid &&
                          ((String.IsNullOrEmpty(taqninmeta.geographic_person) && String.IsNullOrEmpty(taqninmeta.studentUser) || taqninmeta.BackToGeographic == 1) && taqnin.income_no == id) && taqninmeta.SuspendedOrder == false
                            select new systemdata
                            {
                                id_no = taqnin.id_no
                                ,
                                id = taqninmeta.id,
                                Taqninid = taqnin.Taqninid
                                ,
                                w_man = taqnin.w_man
                                ,
                                activity = taqnin.activity,
                                governate = taqnin.governate,
                                actualarea = taqnin.actualarea,
                                income_no = taqnin.income_no,
                                tazalom = taqnin.tazalom,
                                area = taqnin.area,
                                shapearea = taqnin.shapearea,
                                status = taqnin.status,
                                study_note = taqnin.study_note,
                                name = taqnin.name,
                                unit = taqnin.unit,
                                uploaddate = taqninmeta.uploaddate,
                                responsedate = taqninmeta.responsedate
                            };


                return View(query.ToList());
            }
            else
                return RedirectToAction("Login", "Account");
        }


        public ActionResult GeographicData(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var taqninmeta = db.TaqninMetadata.FirstOrDefault(x => x.id == id);
                var taqninmeta_gpr = taqninmeta.geographic_person_response;

                List<string> tt = new List<string>
            {
                "خارج", "داخل"
            };

                ViewBag.Geo_person_response = new SelectList(tt, taqninmeta_gpr);

                return View(taqninmeta);
            }
            else
                return RedirectToAction("Login", "Account");
        }
        public ActionResult sendtostudyOrders(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var taqninmeta = db.TaqninMetadata.Where(x => x.TaqninData.income_no == id).ToList();

                taqninmeta.ForEach(y =>
                {
                    y.geographic_person = System.Web.HttpContext.Current.User.Identity.Name;
                    y.OrderStatus = "قرار مركز المتغيرات";
                    y.AcceptOrNot = "موافق";
                });


                var incomedata = db.Income_noData.FirstOrDefault(x => x.income_no == id);
                if (incomedata.PoineerApproval == "ارجاع")
                {
                    var nlist = taqninmeta.Where(z => z.PoineerApproval == "ارجاع" && z.BackToGeographic == 1).ToList();
                    nlist.ForEach(y =>
                    {
                        y.BackToVariableCenter = 1;
                        y.BackToGeographic = 0;
                    });
                    foreach (var item in nlist)
                    {
                        var log = new LogTable();
                        log.userName = System.Web.HttpContext.Current.User.Identity.Name;
                        log.id_no = item.TaqninData.id_no;
                        log.action = "قرار مركز المتغيرات";
                        log.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                        db.LogTable.Add(log);
                    }

                }
                else
                {
                    foreach (var item in taqninmeta)
                    {
                        var log = new LogTable();
                        log.userName = System.Web.HttpContext.Current.User.Identity.Name;
                        log.id_no = item.TaqninData.id_no;
                        log.action = "قرار مركز المتغيرات";
                        log.Date = DateTime.Now.ToString("dd/MM/yyyy");
                        db.LogTable.Add(log);
                    }
                }
                //var zz = db.TaqninMetadata.Where(x => x.id == 281).ToList()[0];
                db.Income_noData.Remove(incomedata);
                db.SaveChanges();
                return RedirectToAction("geographiccenteruser");
            }
            else
                return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        public ActionResult sendtostudyOrders1(TaqninMetadata taqninmeta, string previewdate, string Imagedate, List<HttpPostedFileBase> ImageBefore, List<HttpPostedFileBase> ImageAfter, HttpPostedFileBase Documents, string buttonaction)
        {
            DirectoryInfo di = null;
            string UniqueFileName = "";
            string UniqueFileName1 = "";
            string UniqueFileName2 = "";
            var taqninmetadata = db.TaqninMetadata.FirstOrDefault(x => x.id == taqninmeta.id);
            string Folderpath = (@"F:\MAI\DocumentsAndImages\") + (taqninmeta.TaqninData.income_no).ToString() + "_" + (taqninmeta.TaqninData.id_no).ToString();//  E:/mai/
            var log = new LogTable();
            log.userName = System.Web.HttpContext.Current.User.Identity.Name;
            log.id_no = taqninmetadata.TaqninData.id_no;

            if (!System.IO.Directory.Exists(Folderpath))
            {
                di = Directory.CreateDirectory(Folderpath);

                for (int i = 0; i < ImageBefore.Count; i++)
                {
                    if (ImageBefore[0] != null)
                    {

                        UniqueFileName = "Before" + "_" + (taqninmeta.Taqninid).ToString() + "_" + i.ToString() + "_" + ImageBefore[i].FileName;
                        string path = Path.Combine(Folderpath, UniqueFileName);
                        ImageBefore[i].SaveAs(path);
                    }
                }
                taqninmetadata.ImageBeforeCounter = ImageBefore.Count;



                for (int i = 0; i < ImageAfter.Count; i++)
                {
                    if (ImageAfter[0] != null)
                    {
                        UniqueFileName1 = "After" + "_" + (taqninmeta.Taqninid).ToString() + "_" + i.ToString() + "_" + ImageAfter[i].FileName;
                        string path = Path.Combine(Folderpath, UniqueFileName1);
                        ImageAfter[i].SaveAs(path);
                    }
                }
                taqninmetadata.ImageAfterCounter = ImageAfter.Count;
               


                if (Documents != null)
                {
                    UniqueFileName2 = "Document" + "_" + (taqninmeta.Taqninid).ToString() + "_" + Documents.FileName;
                    string path = Path.Combine(Folderpath, UniqueFileName2);
                    Documents.SaveAs(path);

                }
            }
            else
            {
                if (ImageBefore[0] != null)
                {
                    System.IO.DirectoryInfo dii = new DirectoryInfo(Folderpath);

                    foreach (FileInfo file in dii.GetFiles())
                    {
                        if (file.FullName.Contains("Before"))
                            file.Delete();
                    }
                    taqninmetadata.ImageBeforeCounter = 0;
                    for (int i = 0; i < ImageBefore.Count; i++)
                    {


                        UniqueFileName = "Before" + "_" + (taqninmeta.Taqninid).ToString() + "_" + taqninmetadata.ImageBeforeCounter.ToString() + ImageBefore[i].FileName;
                        taqninmetadata.ImageBeforeCounter += 1;
                        string path = Path.Combine(Folderpath, UniqueFileName);
                        ImageBefore[i].SaveAs(path);

                    }
                }

                if (ImageAfter[0] != null)
                {
                    System.IO.DirectoryInfo dii = new DirectoryInfo(Folderpath);

                    foreach (FileInfo file in dii.GetFiles())
                    {
                        if (file.FullName.Contains("After"))
                            file.Delete();
                    }
                    taqninmetadata.ImageAfterCounter = 0;
                    for (int i = 0; i < ImageAfter.Count; i++)
                    {


                        UniqueFileName1 = "After" + "_" + (taqninmeta.Taqninid).ToString() + "_" + taqninmetadata.ImageAfterCounter.ToString() + ImageAfter[i].FileName;
                        taqninmetadata.ImageAfterCounter += 1;
                        string path = Path.Combine(Folderpath, UniqueFileName1);
                        ImageAfter[i].SaveAs(path);

                    }

                }


                if (Documents != null)
                {
                    System.IO.DirectoryInfo dii = new DirectoryInfo(Folderpath);
                    foreach (FileInfo file in dii.GetFiles())
                    {
                        if (file.FullName.Contains("Document"))
                            file.Delete();
                    }
                    UniqueFileName2 = "Document" + "_" + (taqninmeta.Taqninid).ToString() + "_" + Documents.FileName;
                    string path = Path.Combine(Folderpath, UniqueFileName2);
                    Documents.SaveAs(path);

                }



            }


            taqninmetadata.ReviewFlag = false;
            if (!String.IsNullOrEmpty(taqninmeta.studynotes))
                taqninmetadata.studynotes = taqninmeta.studynotes;
            taqninmetadata.TaqninData.actualarea = taqninmeta.TaqninData.actualarea;
            if (!String.IsNullOrEmpty(taqninmeta.InspectionMeasure))
                taqninmetadata.InspectionMeasure = taqninmeta.InspectionMeasure;
            if (!String.IsNullOrEmpty(taqninmeta.TaqninData.tazalom))
                taqninmetadata.TaqninData.tazalom = taqninmeta.TaqninData.tazalom;
            if (!String.IsNullOrEmpty(taqninmeta.ChangesCenterDescion))
                taqninmetadata.ChangesCenterDescion = taqninmeta.ChangesCenterDescion;

            taqninmetadata.TaqninData.area = taqninmeta.TaqninData.area;
            if (!String.IsNullOrEmpty(taqninmeta.PreviewDate))
                taqninmetadata.PreviewDate = taqninmeta.PreviewDate;



            if (!String.IsNullOrEmpty(Imagedate))
                taqninmetadata.Imagedate = taqninmeta.Imagedate;
            log.action = "قرار مركز المتغيرات";
            taqninmetadata.studentUser = System.Web.HttpContext.Current.User.Identity.Name;
            if (buttonaction == "ارسال للطلبات المؤجله")
            {
                taqninmetadata.SuspendedOrder = true;
                taqninmetadata.suspendedBy = "variablescenter";
                taqninmetadata.OrderStatus = "طلب مؤجل";


            }
            else
            {
                if (taqninmeta.ChangesCenterDescion == "غيرمستوفي")
                {
                    taqninmetadata.D223AndQM = taqninmeta.D223AndQM;
                    taqninmetadata.landspace = taqninmeta.landspace;
                    taqninmetadata.NotauditedActivity = false;
                    taqninmetadata.Spaceaudit = false;
                    taqninmetadata.Surveyingliftauditing = false;
                    taqninmetadata.NotEnteringCoordinates = false;
                    taqninmetadata.NoAttachments = false;
                    taqninmetadata.NotApplicableCoordinates = false;
                    taqninmetadata.LegalFullfied = null;
                    taqninmetadata.AreaFullfied = null;

                    taqninmetadata.OrderStatus = "اعتماد المراجع";

                }
                else if (taqninmeta.ChangesCenterDescion == "مستوفي")
                {
                    if (!String.IsNullOrEmpty(taqninmeta.LegalFullfied))
                        taqninmetadata.LegalFullfied = taqninmeta.LegalFullfied;
                    if (!String.IsNullOrEmpty(taqninmeta.AreaFullfied))
                        taqninmetadata.AreaFullfied = taqninmeta.AreaFullfied;
                    taqninmetadata.NotauditedActivity = false;
                    taqninmetadata.Spaceaudit = false;
                    taqninmetadata.Surveyingliftauditing = false;
                    taqninmetadata.NotEnteringCoordinates = false;
                    taqninmetadata.NoAttachments = false;
                    taqninmetadata.NotApplicableCoordinates = false;
                }
                else if (taqninmeta.ChangesCenterDescion == "غيرمدقق")
                {
                    taqninmetadata.OrderStatus = "اعتماد رئيس قسم استرداد اراضي الدوله";

                    taqninmetadata.NotauditedActivity = taqninmeta.NotauditedActivity;
                    taqninmetadata.Spaceaudit = taqninmeta.Spaceaudit;
                    taqninmetadata.Surveyingliftauditing = taqninmeta.Surveyingliftauditing;
                    taqninmetadata.NotEnteringCoordinates = taqninmeta.NotEnteringCoordinates;
                    taqninmetadata.NoAttachments = taqninmeta.NoAttachments;
                    taqninmetadata.NotApplicableCoordinates = taqninmeta.NotApplicableCoordinates;
                    taqninmetadata.LegalFullfied = null;
                    taqninmetadata.AreaFullfied = null;
                    taqninmetadata.D223AndQM = false;
                    taqninmetadata.landspace = false;
                }
                if (taqninmetadata.ReviewerApproval == "ارجاع" && String.IsNullOrEmpty(taqninmeta.captianUser) && ((taqninmetadata.ChangesCenterDescion == "مستوفي" && taqninmetadata.LegalFullfied == "بعد القانون") || (taqninmetadata.ChangesCenterDescion == "غيرمستوفي")))
                {
                    taqninmetadata.BackToReviewer = 1;
                    taqninmetadata.OrderStatus = "اعتماد المراجع";
                }
                else if (taqninmetadata.CaptianApproval == "ارجاع" || (taqninmetadata.PoineerApproval == "ارجاع" || taqninmetadata.MajorApproval == "ارجاع" && taqninmetadata.BackToVariableCenter == 1) || (taqninmetadata.ResponseApproval == "ارجاع" && taqninmetadata.BackToVariableCenter == 1))
                {
                    if ((taqninmetadata.ChangesCenterDescion == "مستوفي" && taqninmetadata.LegalFullfied == "قبل القانون") || (taqninmetadata.ChangesCenterDescion == "غيرمدقق"))
                    {
                        taqninmetadata.BackToCaptian = 1;
                        taqninmetadata.OrderStatus = "اعتماد رئيس قسم استرداد اراضي الدوله";

                    }
                    else if ((taqninmetadata.ChangesCenterDescion == "مستوفي" && taqninmetadata.LegalFullfied == "بعد القانون") || (taqninmetadata.ChangesCenterDescion == "غيرمستوفي"))
                    {
                        taqninmetadata.BackToReviewer = 1;
                        taqninmetadata.OrderStatus = "اعتماد المراجع";

                    }
                    else if (String.IsNullOrEmpty(taqninmetadata.ReviewerApproval) && (taqninmetadata.ChangesCenterDescion == "مستوفي" && taqninmetadata.LegalFullfied == "بعد القانون") || (taqninmetadata.ChangesCenterDescion == "غيرمستوفي"))
                    {

                        taqninmetadata.OrderStatus = "اعتماد المراجع";

                    }
                    else if ((((taqninmetadata.ChangesCenterDescion == "مستوفي" && taqninmetadata.LegalFullfied == "قبل القانون")) || (taqninmetadata.ChangesCenterDescion == "غيرمدقق")) || (taqninmetadata.ReviewerApproval == "موافق") && String.IsNullOrEmpty(taqninmetadata.CaptianApproval) && String.IsNullOrEmpty(taqninmetadata.captianUser))
                    {
                        taqninmetadata.OrderStatus = "اعتماد رئيس قسم استرداد اراضي الدوله";
                    }
                }
                else
                {
                    if ((taqninmetadata.ChangesCenterDescion == "مستوفي" && taqninmetadata.LegalFullfied == "قبل القانون") || (taqninmetadata.ChangesCenterDescion == "غيرمدقق"))
                    {
                        taqninmetadata.BackToCaptian = 1;
                        taqninmetadata.OrderStatus = "اعتماد رئيس قسم استرداد اراضي الدوله";

                    }
                    else if ((taqninmetadata.ChangesCenterDescion == "مستوفي" && taqninmetadata.LegalFullfied == "بعد القانون") || (taqninmetadata.ChangesCenterDescion == "غيرمستوفي"))
                    {
                        taqninmetadata.BackToReviewer = 1;
                        taqninmetadata.OrderStatus = "اعتماد المراجع";

                    }
                }

            }

            log.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            db.LogTable.Add(log);
            taqninmetadata.BackToVariableCenter = 0;
            taqninmetadata.TaqninData.Updated = System.Web.HttpContext.Current.User.Identity.Name;
            taqninmetadata.TaqninData.UpdatedDevice = DetermineCompName();
            taqninmetadata.TaqninData.UpdatedTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            db.SaveChanges();
            return RedirectToAction("Variables_center_decision");
        }

        public ActionResult ReferencesApproval(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom1,
            string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity, string revieweruser, string LegalFullfied, int? searchh, string ChangesCenterDescion1,string suspend,int ? page)
        {
         //  insertdata();

            int pageindex = 1;
            int pagesize = 20;
            pageindex = page.HasValue ? Convert.ToInt32(page) : pageindex;
            //func();
            if (User.Identity.IsAuthenticated)
            {
                var dictionary = new Dictionary<string, SelectList>();

                ViewBag.actioname = "ReferencesApproval";
                ViewBag.users = new SelectList(GetUsers().ToList());
                ViewBag.governamentlst = new SelectList(GetGovernament().ToList());
                ViewBag.incomelst = new SelectList(GetIncome().ToList());
                ViewBag.Status1 = new SelectList(Status().ToList());
                ViewBag.activity1 = new SelectList(activitylst().ToList());
                ViewBag.studyUser1 = new SelectList(studyUser().ToList());
                ViewBag.Revieweruser1 = new SelectList(Revieweruser().ToList());
                ViewBag.LegalFullfied1 = new SelectList(LegalFullfiedlst().ToList());



                ViewBag.idnoselected = id_no;
                ViewBag.income_noselected = income_no;
                ViewBag.geographic_person_responseselected = geographic_person_response;
                ViewBag.governateselected = governate;
                ViewBag.studentUserselected = studentUser;
                ViewBag.Descion223selected = Descion223;
                ViewBag.nameselected = name;
                ViewBag.DescionQMselected = DescionQM;
                ViewBag.responsedateselected = responsedate;
                ViewBag.tazalomselected = tazalom1;
                ViewBag.RaiseSurveyorsselected = RaiseSurveyors;
                ViewBag.fullfiltermsselected = fullfilterms;
                ViewBag.statusselected = status;
                ViewBag.Delayedselected = Delayed;
                ViewBag.activityselected = activity;
                ViewBag.revieweruserselected = revieweruser;
                ViewBag.LegalFullfiedselected = LegalFullfied;
                ViewBag.ChangesCenterDescionselected = ChangesCenterDescion1;
                ViewBag.suspendedselected = suspend;
                var lst = new List<refrenceapprovaldata>();
               
                    lst = searchbyrefrenceapproval(id_no, income_no, geographic_person_response, governate, studentUser, Descion223, name, DescionQM, responsedate, tazalom1,
                    RaiseSurveyors, fullfilterms, status, Delayed, activity, revieweruser, LegalFullfied, ChangesCenterDescion1);
                  
                    if (suspend == "مؤجل") { lst = lst.Where(y => y.ReviewerSuspended == "مؤجل").ToList(); }
                    else if (suspend == "غير مؤجل") { lst = lst.Where(y => string.IsNullOrEmpty(y.ReviewerSuspended)).ToList(); }

                    ViewBag.count = lst.Count;
                    return View(lst.ToPagedList(pageindex, pagesize));
            }
            else
                return RedirectToAction("Login", "Account");
        }

        public ActionResult ReviewerData(int id, string governate, string income_no, string studentUser, string activity, string name, string revieweruser, string geographic_person_response, string ChangesCenterDescion, string Delayed, string LegalFullfied, string tazalom1, string Descion223, string DescionQM, string RaiseSurveyors, string fullfilterms, string status, string responsedate)
        {
            if (User.Identity.IsAuthenticated)
            {
                var taqninmeta = db.TaqninMetadata.FirstOrDefault(x => x.id == id);
                var taqninmeta_gpr = taqninmeta.geographic_person_response;

                List<string> tt = new List<string>
            {
                "خارج", "داخل"
            };

                ViewBag.Geo_person_response = new SelectList(tt, taqninmeta_gpr);
                ViewBag.governate = governate;
                ViewBag.income_no = income_no;
                ViewBag.studentUser = studentUser;
                ViewBag.activity = activity;
                ViewBag.name = name;
                ViewBag.revieweruser = revieweruser;
                ViewBag.geographic_person_response = geographic_person_response;
                ViewBag.ChangesCenterDescion = ChangesCenterDescion;
                ViewBag.Delayed = Delayed;
                ViewBag.LegalFullfied = LegalFullfied;
                ViewBag.tazalom1 = tazalom1;
                ViewBag.Descion223 = Descion223;
                ViewBag.DescionQM = DescionQM;
                ViewBag.RaiseSurveyors = RaiseSurveyors;
                ViewBag.fullfilterms = fullfilterms;
                ViewBag.responsedate = responsedate;

                ViewBag.status = status;
                return View(taqninmeta);
            }
            return RedirectToAction("Login", "Account");
        }
        //before
        public ActionResult showimagesBefore(int id)
        {
            var taqninMeta = db.TaqninMetadata.FirstOrDefault(x => x.TaqninData.Taqninid == id);
            return View(taqninMeta);
        }
        //after

        public ActionResult ShowImagesAfter(int id)
        {
            var taqninMeta = db.TaqninMetadata.FirstOrDefault(x => x.TaqninData.Taqninid == id);
            return View(taqninMeta);
        }
        public FileResult ShowImages(int id, string counter, string type, string incomeno)
        {
            var taqnnin = db.TaqninData.SingleOrDefault(x => x.Taqninid == id);
            string Folderpath = (@"F:\MAI\DocumentsAndImages\") + incomeno + "_" + taqnnin.id_no.ToString();//  E:/mai/

            List<images> imglst = new List<images>();
            var pa = "";

            System.IO.DirectoryInfo di = new DirectoryInfo(Folderpath);

            foreach (FileInfo file in di.GetFiles())
            {
                if (file.Name.Contains(type + "_" + id.ToString() + "_" + counter))
                {
                    pa = Path.Combine(Folderpath, file.Name);

                }
            }
            if (pa == "") { return null; }

            return base.File(pa, "image/jpeg");


        }

        public ActionResult GetReport(int id)
        {
            var model = db.TaqninData.FirstOrDefault(x => x.Taqninid == id);
            string Folderpath = (@"F:\MAI\DocumentsAndImages\") + (model.income_no).ToString() + "_" + (model.id_no).ToString();


            System.IO.DirectoryInfo di = new DirectoryInfo(Folderpath);

            foreach (FileInfo file in di.GetFiles())
            {
                if (file.Name.Contains("Document" + "_" + id.ToString()))
                {

                    string ReportURL = Path.Combine(Folderpath, file.Name);
                    byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
                    return File(FileBytes, "application/pdf");
                }
            }
            return Content("لا يوجد مرفقات");
        }

        [HttpPost]

        public ActionResult SaveReviewerData(TaqninMetadata taqninmeta, string ReviewerNotes, string buttonaction, FormCollection frm, string approve, string income_no1, string governate1, string studentUser1, string activity1, string name1, string revieweruser1, string geographic_person_response1, string ChangesCenterDescion1, string Delayed1, string LegalFullfied1, string tazalom1, string DescionQM1, string Descion2231, string RaiseSurveyors1, string fullfilterms1, string status1, string responsedate1, bool ReviewerSuspended)
        {
            var taqninmetadatasaved = db.TaqninMetadata.FirstOrDefault(x => x.id == taqninmeta.id);

            var log = new LogTable();
            log.userName = System.Web.HttpContext.Current.User.Identity.Name;
            log.id_no = taqninmetadatasaved.TaqninData.id_no;

            log.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            log.action = "اعتماد المراجع";
            taqninmetadatasaved.ReviewerSuspended = ReviewerSuspended == true ? "مؤجل" : "";
            taqninmetadatasaved.ReviewerNotes = ReviewerNotes == null ? "" : ReviewerNotes.Trim();
            if (ReviewerSuspended)
            {

                db.SaveChanges();
                // var d = db.TaqninMetadata.FirstOrDefault(x => x.id == taqninmeta.id);
                return RedirectToAction("ReferencesApproval");
            }
            else
            {


                if (buttonaction == "ارسال للطلبات المؤجله")
                {
                    taqninmetadatasaved.OrderStatus = "طلب مؤجل";
                    taqninmetadatasaved.SuspendedOrder = true;
                    taqninmetadatasaved.suspendedBy = "reviewer";
                }
                //taqninmetadatasaved.ReviewFlag = true;
                else
                {
                    if (taqninmeta.ReviewerApproval == "ارجاع")
                    {

                        taqninmetadatasaved.BackToVariableCenter = 1;
                        taqninmetadatasaved.BackToCaptian = 0;
                        taqninmetadatasaved.BackToPoineer = 0;
                        taqninmetadatasaved.BackToresponse = 0;
                        taqninmetadatasaved.OrderStatus = "قرار مركز المتغيرات";
                    }
                    else
                    {
                        if (taqninmetadatasaved.CaptianApproval == "ارجاع" || taqninmetadatasaved.ResponseApproval == "ارجاع")
                        {
                            taqninmetadatasaved.BackToCaptian = 1;

                            taqninmetadatasaved.BackToVariableCenter = 0;

                            taqninmetadatasaved.BackToPoineer = 0;
                            taqninmetadatasaved.BackToresponse = 0;
                            taqninmetadatasaved.OrderStatus = "اعتماد رئيس قسم استرداد اراضي الدوله";
                        }

                        else
                        {
                            taqninmetadatasaved.BackToCaptian = 1;
                            taqninmetadatasaved.BackToVariableCenter = 0;

                            taqninmetadatasaved.BackToPoineer = 0;
                            taqninmetadatasaved.BackToresponse = 0;
                            taqninmetadatasaved.OrderStatus = "اعتماد رئيس قسم استرداد اراضي الدوله";
                        }
                    }
                }
                //taqninmetadatasaved.BackToCaptian = 1;
                taqninmetadatasaved.BackToReviewer = 0;
                taqninmetadatasaved.Reviewer = System.Web.HttpContext.Current.User.Identity.Name;
                taqninmetadatasaved.ReviewerApproval = taqninmeta.ReviewerApproval == null ? approve : frm["ReviewerApproval"];
                //taqninmetadatasaved.ChangesCenterDescion = taqninmeta.ChangesCenterDescion;

                taqninmetadatasaved.ReviewerNotes = ReviewerNotes.Trim();
                taqninmetadatasaved.TaqninData.Updated = System.Web.HttpContext.Current.User.Identity.Name;
                taqninmetadatasaved.TaqninData.UpdatedDevice = DetermineCompName();
                taqninmetadatasaved.TaqninData.UpdatedTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");

                db.LogTable.Add(log);
                db.SaveChanges();
            }
            return RedirectToAction("ReferencesApproval", new { income_no = income_no1, geographic_person_response = geographic_person_response1, governate = governate1, studentUser = studentUser1, Descion223 = Descion2231, name = name1, DescionQM = DescionQM1, responsedate = responsedate1, tazalom1 = tazalom1, RaiseSurveyors = RaiseSurveyors1, fullfilterms = fullfilterms1, status = status1, activity = activity1, revieweruser = revieweruser1, Delayed = Delayed1, LegalFullfied = LegalFullfied1, ChangesCenterDescion = ChangesCenterDescion1 });
        }

        public ActionResult Closewindow()
        {
            return View();
        }
        /// <summary>
        /// النقيب
        /// </summary>
        /// <returns></returns>
        public ActionResult RecoveryDepartmentApproval(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom1,
            string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity1, int? searchh, string revieweruser, string LegalFullfied, string ChangesCenterDescion1,int ? page)
        {
            int pageindex = 1;
            int pagesize = 20;
            pageindex = page.HasValue ? Convert.ToInt32(page) : pageindex;
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.actioname = "RecoveryDepartmentApproval";

                ViewBag.users = new SelectList(GetUsers().ToList());
                ViewBag.governamentlst = new SelectList(GetGovernament().ToList());
                ViewBag.incomelst = GetIncome().Select(r => new SelectListItem { Text = r, Value = r });
                ViewBag.Status1 = new SelectList(Status().ToList());
                ViewBag.activity1 = new SelectList(activitylst().ToList());
                ViewBag.studyUser1 = new SelectList(studyUser().ToList());
                ViewBag.Revieweruser1 = new SelectList(Revieweruser().ToList());
                ViewBag.LegalFullfied1 = new SelectList(LegalFullfiedlst().ToList());
                var lst = new List<CaptianData>();
                ViewBag.idnoselected = id_no;
                ViewBag.income_noselected = income_no;
                ViewBag.geographic_person_responseselected = geographic_person_response;
                ViewBag.governateselected = governate;
                ViewBag.studentUserselected = studentUser;
                ViewBag.Descion223selected = Descion223;
                ViewBag.nameselected = name;
                ViewBag.DescionQMselected = DescionQM;
                ViewBag.responsedateselected = responsedate;
                ViewBag.tazalomselected = tazalom1;
                ViewBag.RaiseSurveyorsselected = RaiseSurveyors;
                ViewBag.fullfiltermsselected = fullfilterms;
                ViewBag.statusselected = status;
                ViewBag.Delayedselected = Delayed;
                ViewBag.activityselected = activity1;
                ViewBag.revieweruserselected = revieweruser;
                ViewBag.LegalFullfiedselected = LegalFullfied;
                ViewBag.ChangesCenterDescionselected = ChangesCenterDescion1;


                lst = searchby3(id_no, income_no, geographic_person_response, governate, studentUser, Descion223, name, DescionQM, responsedate, tazalom1,
                RaiseSurveyors, fullfilterms, status, Delayed, activity1, revieweruser, LegalFullfied, ChangesCenterDescion1);
                ViewBag.count = lst.Count;
                
                return View(lst.ToPagedList(pageindex, pagesize));



               


            }



            else
                return RedirectToAction("Login", "Account");
        }


        public ActionResult RecoveryDepartmentData(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var taqninmeta = db.TaqninMetadata.FirstOrDefault(x => x.id == id);
                var taqninmeta_gpr = taqninmeta.geographic_person_response;

                List<string> tt = new List<string>
            {
                "خارج", "داخل"
            };

                ViewBag.Geo_person_response = new SelectList(tt, taqninmeta_gpr);

                return View(taqninmeta);
            }
            else
                return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult SaveCaptianData(TaqninMetadata taqninmeta, string buttonaction, FormCollection frm, string approve)
        {
            var taqninmetadatasaved = db.TaqninMetadata.FirstOrDefault(x => x.id == taqninmeta.id);

            var log = new LogTable();
            log.userName = System.Web.HttpContext.Current.User.Identity.Name;
            log.id_no = taqninmetadatasaved.TaqninData.id_no;
            log.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            log.action = "اعتماد رئيس قسم استرداد اراضي الدوله";

            taqninmetadatasaved.BackToReviewercheck = 0;
            taqninmetadatasaved.ReviewercheckApproval = "";

            if (taqninmeta.ChangesCenterDescion == "غيرمستوفي")
            {
                taqninmetadatasaved.D223AndQM = taqninmeta.D223AndQM;
                taqninmetadatasaved.landspace = taqninmeta.landspace;
                taqninmetadatasaved.NotauditedActivity = false;
                taqninmetadatasaved.Spaceaudit = false;
                taqninmetadatasaved.Surveyingliftauditing = false;
                taqninmetadatasaved.NotEnteringCoordinates = false;
                taqninmetadatasaved.NoAttachments = false;
                taqninmetadatasaved.NotApplicableCoordinates = false;
                taqninmetadatasaved.LegalFullfied = null;
                taqninmetadatasaved.AreaFullfied = null;
            }
            else if (taqninmeta.ChangesCenterDescion == "مستوفي")
            {
                if (!String.IsNullOrEmpty(taqninmeta.LegalFullfied))
                    taqninmetadatasaved.LegalFullfied = taqninmeta.LegalFullfied;
                if (!String.IsNullOrEmpty(taqninmeta.AreaFullfied))
                    taqninmetadatasaved.AreaFullfied = taqninmeta.AreaFullfied;
                taqninmetadatasaved.NotauditedActivity = false;
                taqninmetadatasaved.Spaceaudit = false;
                taqninmetadatasaved.Surveyingliftauditing = false;
                taqninmetadatasaved.NotEnteringCoordinates = false;
                taqninmetadatasaved.NoAttachments = false;
                taqninmetadatasaved.NotApplicableCoordinates = false;
            }
            else if (taqninmeta.ChangesCenterDescion == "غيرمدقق")
            {
                taqninmetadatasaved.NotauditedActivity = taqninmeta.NotauditedActivity;
                taqninmetadatasaved.Spaceaudit = taqninmeta.Spaceaudit;
                taqninmetadatasaved.Surveyingliftauditing = taqninmeta.Surveyingliftauditing;
                taqninmetadatasaved.NotEnteringCoordinates = taqninmeta.NotEnteringCoordinates;
                taqninmetadatasaved.NoAttachments = taqninmeta.NoAttachments;
                taqninmetadatasaved.NotApplicableCoordinates = taqninmeta.NotApplicableCoordinates;
                taqninmetadatasaved.LegalFullfied = null;
                taqninmetadatasaved.AreaFullfied = null;
                taqninmetadatasaved.D223AndQM = false;
                taqninmetadatasaved.landspace = false;
            }
            taqninmetadatasaved.captianFlag = true;


            taqninmetadatasaved.captianUser = System.Web.HttpContext.Current.User.Identity.Name;
            taqninmetadatasaved.CaptianApproval = frm["CaptianApproval"] == null ? approve : frm["CaptianApproval"];

            if (buttonaction == "ارسال للطلبات المؤجله")
            {
                taqninmetadatasaved.SuspendedOrder = true;
                taqninmetadatasaved.suspendedBy = "captian";
                taqninmetadatasaved.OrderStatus = "طلب مؤجل";

            }
            else
            {
                if (taqninmetadatasaved.CaptianApproval == "ارجاع" && ((taqninmeta.ChangesCenterDescion == "غيرمدقق" || ((taqninmeta.ChangesCenterDescion == "مستوفي") && taqninmeta.LegalFullfied == "قبل القانون"))))
                {
                    taqninmetadatasaved.BackToVariableCenter = 1;

                    taqninmetadatasaved.OrderStatus = "قرار مركز المتغيرات";
                }
                else if (taqninmetadatasaved.CaptianApproval == "ارجاع" && (taqninmeta.ChangesCenterDescion == "غيرمستوفي" || ((taqninmeta.ChangesCenterDescion == "مستوفي") && taqninmeta.LegalFullfied == "بعد القانون")))
                {
                    taqninmetadatasaved.BackToReviewer = 1;
                    taqninmetadatasaved.OrderStatus = "اعتماد المراجع";

                }
                else if ((taqninmetadatasaved.PoineerApproval == "حفظ" || taqninmetadatasaved.PoineerApproval == "ارجاع") && taqninmetadatasaved.BackToReviewer == 0 && taqninmetadatasaved.BackToVariableCenter == 0 || (taqninmetadatasaved.ResponseApproval == "ارجاع" && taqninmetadatasaved.BackToCaptian == 1))
                {
                    taqninmetadatasaved.BackToPoineer = 1;
                    taqninmetadatasaved.OrderStatus = "اعتماد قائد مركز المتغيرات المكانيه";

                }
                else if (taqninmetadatasaved.CaptianApproval == "موافق")
                {
                    if (!(String.IsNullOrEmpty(taqninmetadatasaved.PoineerApproval)) && taqninmetadatasaved.LegalFullfied == "بعد القانون" && taqninmetadatasaved.ChangesCenterDescion == "مستوفي")
                        taqninmetadatasaved.BackToPoineer = 1;
                    taqninmetadatasaved.OrderStatus = "اعتماد قائد مركز المتغيرات المكانيه";

                }
            }
            taqninmetadatasaved.BackToCaptian = 0;
            taqninmetadatasaved.ChangesCenterDescion = taqninmeta.ChangesCenterDescion;
            taqninmetadatasaved.RecoveryDepartmentNotes = taqninmeta.RecoveryDepartmentNotes;
            taqninmetadatasaved.TaqninData.Updated = System.Web.HttpContext.Current.User.Identity.Name;
            taqninmetadatasaved.TaqninData.UpdatedDevice = DetermineCompName();
            taqninmetadatasaved.TaqninData.UpdatedTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");

            db.LogTable.Add(log);
            db.SaveChanges();
            return RedirectToAction("RecoveryDepartmentApproval");
        }
        //الرائد

        public ActionResult Pioneer(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom1,
            string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity1, string revieweruser, string LegalFullfied, string ChangesCenterDescion1,int?page)
        {
          // func1();
          //func4();
       //  captianfunc();
        //   updatemdbdataall();
            int pageindex = 1;
            int pagesize = 20;
            pageindex = page.HasValue ? Convert.ToInt32(page) : pageindex;
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.actioname = "Pioneer";

                ViewBag.users = new SelectList(GetUsers().ToList());
                ViewBag.governamentlst = new SelectList(GetGovernament().ToList());
                ViewBag.incomelst = GetIncome().Select(r => new SelectListItem { Text = r, Value = r });
                ViewBag.Status1 = new SelectList(Status().ToList());
                ViewBag.activity1 = new SelectList(activitylst().ToList());
                ViewBag.studyUser1 = new SelectList(studyUser().ToList());
                ViewBag.Revieweruser1 = new SelectList(Revieweruser().ToList());
                ViewBag.LegalFullfied1 = new SelectList(LegalFullfiedlst().ToList());
                ViewBag.idnoselected = id_no;
                ViewBag.income_noselected = income_no;
                ViewBag.geographic_person_responseselected = geographic_person_response;
                ViewBag.governateselected = governate;
                ViewBag.studentUserselected = studentUser;
                ViewBag.Descion223selected = Descion223;
                ViewBag.nameselected = name;
                ViewBag.DescionQMselected = DescionQM;
                ViewBag.responsedateselected = responsedate;
                ViewBag.tazalomselected = tazalom1;
                ViewBag.RaiseSurveyorsselected = RaiseSurveyors;
                ViewBag.fullfiltermsselected = fullfilterms;
                ViewBag.statusselected = status;
                ViewBag.Delayedselected = Delayed;
                ViewBag.activityselected = activity1;
                ViewBag.revieweruserselected = revieweruser;
                ViewBag.LegalFullfiedselected = LegalFullfied;
                ViewBag.ChangesCenterDescionselected = ChangesCenterDescion1;
             var   lst = searchbyPoineer(id_no, income_no, geographic_person_response, governate, studentUser, Descion223, name, DescionQM, responsedate, tazalom1,
             RaiseSurveyors, fullfilterms, status, Delayed, activity1, revieweruser, LegalFullfied, ChangesCenterDescion1);
                ViewBag.count = lst.Count;
                return View(lst.ToPagedList(pageindex, pagesize));
            }

            else
                return RedirectToAction("Login", "Account");
        }



        public ActionResult PoineerData(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var taqninmeta = db.TaqninMetadata.FirstOrDefault(x => x.id == id);
                var taqninmeta_gpr = taqninmeta.geographic_person_response;

                List<string> tt = new List<string>
            {
                "خارج", "داخل"
            };

                ViewBag.Geo_person_response = new SelectList(tt, taqninmeta_gpr);

                return View(taqninmeta);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult SavePoineerData(TaqninMetadata taqninmeta, FormCollection frm)
        {
            var taqninmetadatasaved = db.TaqninMetadata.FirstOrDefault(x => x.id == taqninmeta.id);
            var log = new LogTable();
            log.userName = System.Web.HttpContext.Current.User.Identity.Name;
            log.id_no = taqninmetadatasaved.TaqninData.id_no;
            log.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");

            if (taqninmeta.ChangesCenterDescion == "غيرمستوفي")
            {
                taqninmetadatasaved.D223AndQM = taqninmeta.D223AndQM;
                taqninmetadatasaved.landspace = taqninmeta.landspace;
                taqninmetadatasaved.NotauditedActivity = false;
                taqninmetadatasaved.Spaceaudit = false;
                taqninmetadatasaved.Surveyingliftauditing = false;
                taqninmetadatasaved.NotEnteringCoordinates = false;
                taqninmetadatasaved.NoAttachments = false;
                taqninmetadatasaved.NotApplicableCoordinates = false;
                taqninmetadatasaved.LegalFullfied = null;
                taqninmetadatasaved.AreaFullfied = null;
            }
            else if (taqninmeta.ChangesCenterDescion == "مستوفي")
            {
                if (!String.IsNullOrEmpty(taqninmeta.LegalFullfied))
                    taqninmetadatasaved.LegalFullfied = taqninmeta.LegalFullfied;
                if (!String.IsNullOrEmpty(taqninmeta.AreaFullfied))
                    taqninmetadatasaved.AreaFullfied = taqninmeta.AreaFullfied;
                taqninmetadatasaved.NotauditedActivity = false;
                taqninmetadatasaved.Spaceaudit = false;
                taqninmetadatasaved.Surveyingliftauditing = false;
                taqninmetadatasaved.NotEnteringCoordinates = false;
                taqninmetadatasaved.NoAttachments = false;
                taqninmetadatasaved.NotApplicableCoordinates = false;
            }
            else if (taqninmeta.ChangesCenterDescion == "غيرمدقق")
            {
                taqninmetadatasaved.NotauditedActivity = taqninmeta.NotauditedActivity;
                taqninmetadatasaved.Spaceaudit = taqninmeta.Spaceaudit;
                taqninmetadatasaved.Surveyingliftauditing = taqninmeta.Surveyingliftauditing;
                taqninmetadatasaved.NotEnteringCoordinates = taqninmeta.NotEnteringCoordinates;
                taqninmetadatasaved.NoAttachments = taqninmeta.NoAttachments;
                taqninmetadatasaved.NotApplicableCoordinates = taqninmeta.NotApplicableCoordinates;
                taqninmetadatasaved.LegalFullfied = null;
                taqninmetadatasaved.AreaFullfied = null;
                taqninmetadatasaved.D223AndQM = false;
                taqninmetadatasaved.landspace = false;
            }

            taqninmetadatasaved.PoineerUser = System.Web.HttpContext.Current.User.Identity.Name;
            taqninmetadatasaved.ChangesCenterDescion = taqninmeta.ChangesCenterDescion;


            var Descion = frm["descion"];
            log.action = "اعتماد قرار قائد مركز المتغيرات المكانيه";
            switch (Descion)
            {
                case "":
                    taqninmetadatasaved.PoineerApproval = "موافق";
                    taqninmetadatasaved.BackToPoineer = 0;
                    taqninmetadatasaved.BackToMajor = 1;
                    taqninmetadatasaved.OrderStatus = "اعتماد مدير اداره المساحه العسكريه";
                    
                    break;
                case "1":
                    taqninmetadatasaved.BackToGeographic = 1;
                    taqninmetadatasaved.OrderStatus = "دراسه المركز الجغرافي ";
                    taqninmetadatasaved.ReviewerApproval = null;
                    taqninmetadatasaved.CaptianApproval = null;
                    var incomlst = db.Income_noData.Where(x => x.income_no == taqninmetadatasaved.TaqninData.income_no && x.PoineerApproval == "ارجاع").ToList();
                    if (incomlst.Count == 0)
                    {
                        var incomedata = new Income_noData();
                        incomedata.geographicperson = taqninmetadatasaved.geographic_person;
                        incomedata.uploaddate = taqninmetadatasaved.uploaddate;
                        incomedata.PoineerApproval = "ارجاع";
                        incomedata.income_no = taqninmetadatasaved.TaqninData.income_no;
                        if (taqninmetadatasaved.geographic_person_response == "داخل")
                        {
                            incomedata.insideOrdersCount = 1;
                        }
                        else if (taqninmetadatasaved.geographic_person_response == "خارج")
                        {
                            incomedata.outsideOrdersCount = 1;
                        }
                        incomedata.ordersCount = 1;
                        db.Income_noData.Add(incomedata);
                    }
                    else
                    {

                        if (taqninmetadatasaved.geographic_person_response == "داخل")
                        {
                            incomlst[0].insideOrdersCount += 1;
                        }
                        else if (taqninmetadatasaved.geographic_person_response == "خارج")
                        {
                            incomlst[0].outsideOrdersCount += 1;
                        }
                        incomlst[0].ordersCount += 1;
                    }
                    taqninmetadatasaved.PoineerApproval = "ارجاع";
                    db.SaveChanges();
                    break;
                case "2":
                    taqninmetadatasaved.OrderStatus = "قرار مركز المتغيرات";
                    taqninmetadatasaved.BackToVariableCenter = 1;
                    taqninmetadatasaved.PoineerApproval = "ارجاع";
                    break;

                case "3":
                    taqninmetadatasaved.OrderStatus = "اعتماد المراجع";

                    taqninmetadatasaved.BackToReviewer = 1;
                    taqninmetadatasaved.PoineerApproval = "ارجاع";
                    break;

                case "4":
                    taqninmetadatasaved.OrderStatus = "اعتماد رئيس قسم استرداد اراضي الدوله";

                    taqninmetadatasaved.BackToCaptian = 1;
                    taqninmetadatasaved.PoineerApproval = "ارجاع";
                    break;

                case "5":

                    taqninmetadatasaved.PoineerApproval = "الرد";

                    taqninmetadatasaved.OrderStatus = "الرد";
                    if (taqninmetadatasaved.ResponseApproval == "ارجاع")
                        taqninmetadatasaved.BackToresponse = 1;
                    break;

            }
            taqninmetadatasaved.BackToPoineer = 0;
           

            taqninmetadatasaved.PoineerNotes = taqninmeta.PoineerNotes;
            taqninmetadatasaved.TaqninData.Updated = System.Web.HttpContext.Current.User.Identity.Name;
            taqninmetadatasaved.TaqninData.UpdatedDevice = DetermineCompName();
            taqninmetadatasaved.TaqninData.UpdatedTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");

            db.LogTable.Add(log);
            db.SaveChanges();
            return RedirectToAction("Pioneer");
        }
        //اللواء

        public ActionResult Major(string id_no, string income_no, string governate, string studentUser, int? searchh)
        {
            //func();
            if (User.Identity.IsAuthenticated)
            {
               

             
                ViewBag.governamentlst = new SelectList(GetGovernament().ToList());
                ViewBag.incomelst = new SelectList(GetIncome().ToList());
                ViewBag.studyUser1 = new SelectList(studyUser().ToList());
               
                 var   lst = searchbymajor(id_no, income_no, governate, studentUser);
               ViewBag.count = lst.Count();
               
                return View(lst);

            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult MajorData(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var taqninmeta = db.TaqninMetadata.FirstOrDefault(x => x.id == id);
                var taqninmeta_gpr = taqninmeta.geographic_person_response;

                List<string> tt = new List<string>
            {
                "خارج", "داخل"
            };

                ViewBag.Geo_person_response = new SelectList(tt, taqninmeta_gpr);

                return View(taqninmeta);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult SaveMajorData(TaqninMetadata taqninmeta, FormCollection frm)
        {
            var taqninmetadatasaved = db.TaqninMetadata.FirstOrDefault(x => x.id == taqninmeta.id);


            var log = new LogTable();
            log.userName = System.Web.HttpContext.Current.User.Identity.Name;
            log.id_no = taqninmetadatasaved.TaqninData.id_no;
            log.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            log.action = "اعتماد مدير اداره المساحه العسكريه";
            taqninmetadatasaved.majorUser = System.Web.HttpContext.Current.User.Identity.Name;
            taqninmetadatasaved.MajorNotes = taqninmeta.MajorNotes;
            taqninmetadatasaved.MajorApproval = taqninmeta.MajorApproval;
            if (taqninmetadatasaved.MajorApproval == "ارجاع")
            {
                taqninmetadatasaved.BackToPoineer = 1;
                taqninmetadatasaved.OrderStatus = "اعتماد قائد مركز المتغيرات المكانيه";

            }
            else
            {

                if (taqninmetadatasaved.ChangesCenterDescion == "مستوفي" && taqninmetadatasaved.LegalFullfied == "بعد القانون")
                {
                    taqninmetadatasaved.OrderStatus = "تأكيد قرار";

                    if (taqninmetadatasaved.ReviewercheckApproval == "ارجاع")
                        taqninmetadatasaved.BackToReviewercheck = 1;

                }
                else
                {
                    taqninmetadatasaved.OrderStatus = "الرد";
                    if (taqninmetadatasaved.ResponseApproval == "ارجاع")
                        taqninmetadatasaved.BackToresponse = 1;
                }

            }
            taqninmetadatasaved.BackToMajor = 0;
            taqninmetadatasaved.TaqninData.Updated = System.Web.HttpContext.Current.User.Identity.Name;
            taqninmetadatasaved.TaqninData.UpdatedDevice = DetermineCompName();
            taqninmetadatasaved.TaqninData.UpdatedTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");

            db.SaveChanges();
            return RedirectToAction("Major");
        }
        public ActionResult ReviewerCheck(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom,
            string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity, int? searchh, string revieweruser, string LegalFullfied, string ChangesCenterDescion)
        {
            if (User.Identity.IsAuthenticated)
            {


                ViewBag.users = new SelectList(GetUsers().ToList());
                ViewBag.governamentlst = new SelectList(GetGovernament().ToList());
                ViewBag.incomelst = GetIncome().Select(r => new SelectListItem { Text = r, Value = r });

                ViewBag.Status1 = new SelectList(Status().ToList());
                ViewBag.activity1 = new SelectList(activitylst().ToList());
                ViewBag.studyUser1 = new SelectList(studyUser().ToList());
                ViewBag.Revieweruser1 = new SelectList(Revieweruser().ToList());
                ViewBag.LegalFullfied1 = new SelectList(LegalFullfiedlst().ToList());
                ViewBag.idnoselected = id_no;
                ViewBag.income_noselected = income_no;

                ViewBag.geographic_person_responseselected = geographic_person_response;
                ViewBag.governateselected = governate;
                ViewBag.studentUserselected = studentUser;
                ViewBag.Descion223selected = Descion223;
                ViewBag.nameselected = name;
                ViewBag.DescionQMselected = DescionQM;
                ViewBag.responsedateselected = responsedate;
                ViewBag.tazalomselected = tazalom;
                ViewBag.RaiseSurveyorsselected = RaiseSurveyors;
                ViewBag.fullfiltermsselected = fullfilterms;
                ViewBag.statusselected = status;
                ViewBag.Delayedselected = Delayed;
                ViewBag.activityselected = activity;
                ViewBag.revieweruserselected = revieweruser;
                ViewBag.LegalFullfiedselected = LegalFullfied;
                ViewBag.ChangesCenterDescionselected = ChangesCenterDescion;
                var lst = new List<TaqninMetadata>();
                if (searchh == 1)
                {
                    lst = searchby(id_no, income_no, geographic_person_response, governate, studentUser, Descion223, name, DescionQM, responsedate, tazalom,
                    RaiseSurveyors, fullfilterms, status, Delayed, activity, revieweruser, LegalFullfied, ChangesCenterDescion);
                    if (lst.Count() != 0)
                    {

                        lst = lst.Where(x => ((x.MajorApproval == "موافق" && (x.BackToReviewercheck == 1 || x.OrderStatus == "تأكيد قرار")))).ToList();

                    }


                    ViewBag.count = lst.Count();
                    return View(lst);
                }

                else
                    lst = db.TaqninMetadata.Where(x => ((x.MajorApproval == "موافق" && (x.BackToReviewercheck == 1 || x.OrderStatus == "تأكيد قرار")))).ToList();

                ViewBag.count = lst.Count();
                return View(lst);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult ReviewerCheckData(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var taqninmeta = db.TaqninMetadata.FirstOrDefault(x => x.id == id);
                var taqninmeta_gpr = taqninmeta.geographic_person_response;

                List<string> tt = new List<string>
            {
                "خارج", "داخل"
            };

                ViewBag.Geo_person_response = new SelectList(tt, taqninmeta_gpr);

                return View(taqninmeta);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult SaveReviewerCheckData(TaqninMetadata taqninmeta, string ResponseNotes, FormCollection frm)
        {

            var taqninmetadatasaved = db.TaqninMetadata.FirstOrDefault(x => x.id == taqninmeta.id);
            var log = new LogTable();
            log.userName = System.Web.HttpContext.Current.User.Identity.Name;
            log.id_no = taqninmetadatasaved.TaqninData.id_no;
            log.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            log.action = "تأكيد قرار";
            var Descion = frm["descion"];
            taqninmetadatasaved.responsedate = DateTime.Now.ToString("dd/MM/yyyy");
            taqninmetadatasaved.responseUser = null;
            taqninmetadatasaved.ResponseNotes = ResponseNotes == null ? "" : ResponseNotes.Trim();
            switch (Descion)
            {
                case "1":
                    taqninmetadatasaved.ReviewercheckApproval = "موافق";

                    taqninmetadatasaved.OrderStatus = "الرد";
                    taqninmetadatasaved.responsestatus = "not response";
                    if (taqninmetadatasaved.ResponseApproval == "ارجاع")
                        taqninmetadatasaved.BackToresponse = 1;
                    taqninmetadatasaved.BackToReviewercheck = 0;
                    taqninmetadatasaved.ReviewercheckApproval = "";
                    break;

                case "2":
                    taqninmetadatasaved.BackToVariableCenter = 1;
                    taqninmetadatasaved.OrderStatus = "قرار مركز المتغيرات";
                    taqninmetadatasaved.responsestatus = "not response";
                    taqninmetadatasaved.ReviewercheckApproval = "ارجاع";
                    break;

                case "3":
                    taqninmetadatasaved.OrderStatus = "اعتمادالمراجع";
                    taqninmetadatasaved.BackToReviewer = 1;
                    taqninmetadatasaved.responsestatus = "not response";
                    taqninmetadatasaved.ReviewercheckApproval = "ارجاع";
                    break;

                case "4":
                    taqninmetadatasaved.OrderStatus = "اعتماد رئيس قسم استرداد اراضي الدوله";
                    taqninmetadatasaved.responsestatus = "not response";
                    taqninmetadatasaved.BackToCaptian = 1;
                    taqninmetadatasaved.ReviewercheckApproval = "ارجاع";
                    break;

                case "5":
                    taqninmetadatasaved.OrderStatus = "اعتماد قائد مركز المتغيرات المكانيه";
                    taqninmetadatasaved.responsestatus = "not response";
                    taqninmetadatasaved.BackToPoineer = 1;
                    taqninmetadatasaved.ResponseApproval = "ارجاع";
                    break;

                case "6":
                    taqninmetadatasaved.OrderStatus = "طلب مؤجل";
                    taqninmetadatasaved.responsestatus = "not response";
                    taqninmetadatasaved.SuspendedOrder = true;
                    taqninmetadatasaved.suspendedBy = "response";
                    break;
            }
            db.LogTable.Add(log);
            taqninmetadatasaved.BackToReviewercheck = 0;
            if (!string.IsNullOrEmpty(taqninmeta.AreaFullfied))
                taqninmetadatasaved.AreaFullfied = taqninmeta.AreaFullfied;
            //taqninmetadatasaved.LegalFullfied = taqninmeta.LegalFullfied;
            taqninmetadatasaved.TaqninData.Updated = System.Web.HttpContext.Current.User.Identity.Name;
            taqninmetadatasaved.TaqninData.UpdatedDevice = DetermineCompName();
            taqninmetadatasaved.TaqninData.UpdatedTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");

            db.SaveChanges();
            return RedirectToAction("ReviewerCheck");
        }
        public ActionResult ResponseDescion(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom,
            string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity1, int? searchh, string revieweruser, string LegalFullfied, string ChangesCenterDescion1,string suspend, int? page)
        {
            int pageindex = 1;
            int pagesize = 20;
            pageindex = page.HasValue ? Convert.ToInt32(page) : pageindex;
            
            //int maxRows = 10;
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.actioname = "ResponseDescion";

                ViewBag.users = new SelectList(GetUsers().ToList());
                ViewBag.governamentlst = new SelectList(GetGovernament().ToList());
                ViewBag.incomelst = GetIncome().Select(r => new SelectListItem { Text = r, Value = r });
                ViewBag.Status1 = new SelectList(Status().ToList());
                ViewBag.activity1 = new SelectList(activitylst().ToList());
                ViewBag.studyUser1 = new SelectList(studyUser().ToList());
                ViewBag.Revieweruser1 = new SelectList(Revieweruser().ToList());
                ViewBag.LegalFullfied1 = new SelectList(LegalFullfiedlst().ToList());

                ViewBag.idnoselected = id_no;
                ViewBag.income_noselected = income_no;
                ViewBag.geographic_person_responseselected = geographic_person_response;
                ViewBag.governateselected = governate;
                ViewBag.studentUserselected = studentUser;
                ViewBag.Descion223selected = Descion223;
                ViewBag.nameselected = name;
                ViewBag.DescionQMselected = DescionQM;
                ViewBag.responsedateselected = responsedate;
                ViewBag.tazalomselected = tazalom;
                ViewBag.RaiseSurveyorsselected = RaiseSurveyors;
                ViewBag.fullfiltermsselected = fullfilterms;
                ViewBag.statusselected = status;
                ViewBag.Delayedselected = Delayed;
                ViewBag.activityselected = activity1;
                ViewBag.revieweruserselected = revieweruser;
                ViewBag.LegalFullfiedselected = LegalFullfied;
                ViewBag.ChangesCenterDescionselected = ChangesCenterDescion1;
                ViewBag.suspendedselected = suspend;
                var lst = new List<ResponseData>();
               

                    lst = searchby5(id_no, income_no, geographic_person_response, governate, studentUser, Descion223, name, DescionQM, responsedate, tazalom,
                    RaiseSurveyors, fullfilterms, status, Delayed, activity1, revieweruser, LegalFullfied, ChangesCenterDescion1);
                    if (suspend == "مؤجل") { lst=lst.Where(y => y.ResponseSuspended == "مؤجل").ToList(); }
                    else if (suspend == "غير مؤجل") { lst = lst.Where(y => string.IsNullOrEmpty(y.ResponseSuspended)).ToList(); }

                
                ViewBag.count = lst.Count;
                    return View(lst.ToPagedList(pageindex, pagesize));
               
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult ResponseData(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var taqninmeta = db.TaqninMetadata.FirstOrDefault(x => x.id == id);
                var taqninmeta_gpr = taqninmeta.geographic_person_response;

                List<string> tt = new List<string>
            {
                "خارج", "داخل"
            };

                ViewBag.Geo_person_response = new SelectList(tt, taqninmeta_gpr);

                return View(taqninmeta);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult SaveResponseData(TaqninMetadata taqninmeta, string ResponseNotes, FormCollection frm, bool ResponseSuspended)
        {

            var taqninmetadatasaved = db.TaqninMetadata.FirstOrDefault(x => x.id == taqninmeta.id);
            var log = new LogTable();
            log.userName = System.Web.HttpContext.Current.User.Identity.Name;
            log.id_no = taqninmetadatasaved.TaqninData.id_no;
            log.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            log.action = "الرد";



            taqninmetadatasaved.ResponseSuspended = ResponseSuspended == true ? "مؤجل" : "";
            taqninmetadatasaved.ResponseNotes = ResponseNotes == null ? "" : ResponseNotes.Trim();
            if (ResponseSuspended)
            {

                db.SaveChanges();
                // var d = db.TaqninMetadata.FirstOrDefault(x => x.id == taqninmeta.id);
                return RedirectToAction("ResponseDescion");
            }
            else
            {
                taqninmetadatasaved.responsedate = DateTime.Now.ToString("dd/MM/yyyy");
                taqninmetadatasaved.responseUser = System.Web.HttpContext.Current.User.Identity.Name;
                taqninmetadatasaved.ResponseNotes = ResponseNotes == null ? "" : ResponseNotes.Trim();
                var Descion = frm["descion"];
                switch (Descion)
                {
                    case "1":
                        if (taqninmetadatasaved.ResponseApproval != "موافق")
                        {
                            taqninmetadatasaved.ResponseApproval = "موافق";
                            taqninmetadatasaved.responsestatus = "response";
                            taqninmetadatasaved.OrderStatus = "تم الرد والحفظ فى الارشيف";
                            taqninmetadatasaved.BackToresponse = 0;
                            taqninmetadatasaved.BackToReviewer = 0;
                            taqninmetadatasaved.BackToVariableCenter = 0;
                            taqninmetadatasaved.BackToPoineer = 0;
                            taqninmetadatasaved.BackToCaptian = 0;
                            string date2 = taqninmetadatasaved.uploaddate;
                            DateTime dt = DateTime.ParseExact("14/11/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            DateTime dt1 = DateTime.ParseExact(date2, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                            if (dt1.Date > dt.Date)
                                createshape(taqninmetadatasaved.id);
                            else
                                updatemdbdata(taqninmetadatasaved.id);

                        }
                        else
                        {
                            taqninmetadatasaved.ResponseApproval = "موافق";
                            taqninmetadatasaved.responsestatus = "response";
                            taqninmetadatasaved.OrderStatus = "تم الرد والحفظ فى الارشيف";
                            taqninmetadatasaved.BackToresponse = 0;
                            taqninmetadatasaved.BackToReviewer = 0;
                            taqninmetadatasaved.BackToVariableCenter = 0;
                            taqninmetadatasaved.BackToPoineer = 0;

                        }
                        break;

                    case "2":
                        taqninmetadatasaved.BackToVariableCenter = 1;
                        taqninmetadatasaved.OrderStatus = "قرار مركز المتغيرات";
                        taqninmetadatasaved.responsestatus = "not response";
                        taqninmetadatasaved.ResponseApproval = "ارجاع";
                         taqninmetadatasaved.BackToresponse = 0;
                            taqninmetadatasaved.BackToReviewer = 0;
                            taqninmetadatasaved.BackToCaptian = 0;
                            taqninmetadatasaved.BackToPoineer = 0;
                        break;

                    case "3":
                        taqninmetadatasaved.OrderStatus = "اعتمادالمراجع";
                        taqninmetadatasaved.BackToReviewer = 1;
                        taqninmetadatasaved.responsestatus = "not response";
                        taqninmetadatasaved.ResponseApproval = "ارجاع";
                         taqninmetadatasaved.BackToresponse = 0;
                         taqninmetadatasaved.BackToCaptian = 0;
                            taqninmetadatasaved.BackToVariableCenter = 0;
                            taqninmetadatasaved.BackToPoineer = 0;
                        break;

                    case "4":
                        taqninmetadatasaved.OrderStatus = "اعتماد رئيس قسم استرداد اراضي الدوله";
                        taqninmetadatasaved.responsestatus = "not response";
                        taqninmetadatasaved.BackToCaptian = 1;
                         taqninmetadatasaved.BackToresponse = 0;
                            taqninmetadatasaved.BackToReviewer = 0;
                            taqninmetadatasaved.BackToVariableCenter = 0;
                            taqninmetadatasaved.BackToPoineer = 0;
                        taqninmetadatasaved.ResponseApproval = "ارجاع";
                        break;

                    case "5":
                        taqninmetadatasaved.OrderStatus = "اعتماد قائد مركز المتغيرات المكانيه";
                        taqninmetadatasaved.responsestatus = "not response";
                        taqninmetadatasaved.BackToPoineer = 1;
                        taqninmetadatasaved.ResponseApproval = "ارجاع";
                         taqninmetadatasaved.BackToresponse = 0;
                            taqninmetadatasaved.BackToReviewer = 0;
                            taqninmetadatasaved.BackToVariableCenter = 0;
                            taqninmetadatasaved.BackToCaptian = 0;
                        break;

                    case "6":
                        taqninmetadatasaved.OrderStatus = "طلب مؤجل";
                        taqninmetadatasaved.responsestatus = "not response";
                        taqninmetadatasaved.SuspendedOrder = true;
                        taqninmetadatasaved.suspendedBy = "response";
                        break;

                    case "7":
                        taqninmetadatasaved.OrderStatus = "تأكيد قرار";
                        taqninmetadatasaved.responsestatus = "not response";
                        taqninmetadatasaved.BackToReviewercheck = 1;
                        taqninmetadatasaved.ResponseApproval = "ارجاع";
                         taqninmetadatasaved.BackToresponse = 0;
                            taqninmetadatasaved.BackToReviewer = 0;
                            taqninmetadatasaved.BackToVariableCenter = 0;
                            taqninmetadatasaved.BackToPoineer = 0;
                        break;
                }
                db.LogTable.Add(log);
                taqninmetadatasaved.BackToresponse = 0;
                if (!string.IsNullOrEmpty(taqninmeta.AreaFullfied))
                    taqninmetadatasaved.AreaFullfied = taqninmeta.AreaFullfied;
                //taqninmetadatasaved.LegalFullfied = taqninmeta.LegalFullfied;
                taqninmetadatasaved.TaqninData.Updated = System.Web.HttpContext.Current.User.Identity.Name;
                taqninmetadatasaved.TaqninData.UpdatedDevice = DetermineCompName();
                taqninmetadatasaved.TaqninData.UpdatedTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");

                db.SaveChanges();
                return RedirectToAction("ResponseDescion");
            }
        }
        public ActionResult archive(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom1,
            string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity1, int? searchh, string revieweruser, string LegalFullfied, string ChangesCenterDescion1,int ? page)
        {

            int pageindex = 1;
            int pagesize = 20;
            pageindex = page.HasValue ? Convert.ToInt32(page) : pageindex;
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.actioname = "archive";

                ViewBag.users = new SelectList(GetUsers().ToList());
                ViewBag.governamentlst = new SelectList(GetGovernament().ToList());
                ViewBag.incomelst = GetIncome().Select(r => new SelectListItem { Text = r, Value = r });
                ViewBag.Status1 = new SelectList(Status().ToList());
                ViewBag.activity1 = new SelectList(activitylst().ToList());
                ViewBag.studyUser1 = new SelectList(studyUser().ToList());
                ViewBag.Revieweruser1 = new SelectList(Revieweruser().ToList());
                ViewBag.LegalFullfied1 = new SelectList(LegalFullfiedlst().ToList());
              
                ViewBag.idnoselected = id_no;
                ViewBag.income_noselected = income_no;
                ViewBag.geographic_person_responseselected = geographic_person_response;
                ViewBag.governateselected = governate;
                ViewBag.studentUserselected = studentUser;
                ViewBag.Descion223selected = Descion223;
                ViewBag.nameselected = name;
                ViewBag.DescionQMselected = DescionQM;
                ViewBag.responsedateselected = responsedate;
                ViewBag.tazalomselected = tazalom1;
                ViewBag.RaiseSurveyorsselected = RaiseSurveyors;
                ViewBag.fullfiltermsselected = fullfilterms;
                ViewBag.statusselected = status;
                ViewBag.Delayedselected = Delayed;
                ViewBag.activityselected = activity1;
                ViewBag.revieweruserselected = revieweruser;
                ViewBag.LegalFullfiedselected = LegalFullfied;
                ViewBag.ChangesCenterDescionselected = ChangesCenterDescion1;

                var lst = new List<Viewmodel.ProcedureData>();
                //if (searchh == 1)
                //{

                    lst = searchby4(id_no, income_no, geographic_person_response, governate, studentUser, Descion223, name, DescionQM, responsedate, tazalom1,
                      RaiseSurveyors, fullfilterms, status, Delayed, activity1, revieweruser, LegalFullfied, ChangesCenterDescion1);

                    ViewBag.count = lst.Count;
                    return View(lst.ToPagedList(pageindex, pagesize));
                //}

                //else
                //{

                //    lst = db.TaqninMetadata.Select(s => new ProcedureData
                //{
                //    id = s.id,
                //    id_no = s.TaqninData.id_no,
                //    name = s.TaqninData.name,
                //    status = s.TaqninData.status,
                //    activity = s.TaqninData.activity
                //    ,
                //    ChangesCenterDescion = s.ChangesCenterDescion,
                //    responsestatus = s.responsestatus,
                //    ResponseApproval = s.ResponseApproval
                //    ,
                //    income_no = s.TaqninData.income_no,
                //    uploaddate = s.uploaddate,
                //    person_upload = s.person_upload,
                //    LegalFullfied = s.LegalFullfied,
                //    tazalom = s.TaqninData.tazalom,
                //    Taqninid = s.Taqninid,
                //    geographic_person_response = s.geographic_person_response,
                //    governate = s.TaqninData.governate,
                //    OrderStatus = s.OrderStatus,
                //    studentUser = s.studentUser
                //}).ToList();

                //    return View(lst);
                //}
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult archiveData(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var taqninmeta = db.TaqninMetadata.FirstOrDefault(x => x.id == id);
                var taqninmeta_gpr = taqninmeta.geographic_person_response;

                List<string> tt = new List<string>
            {
                "خارج", "داخل"
            };

                ViewBag.Geo_person_response = new SelectList(tt, taqninmeta_gpr);

                return View(taqninmeta);
            }
            return RedirectToAction("Login", "Account");
        }


        public FileResult downloadwithsearch(string income_no, string geographic_person_response, string governate, string studentUser, string tazalom,
            string fullfilterms, string status, string activity1, string revieweruser, string LegalFullfied, string ChangesCenterDescion1, string Responsestatus)
        {

            System.Data.DataTable dt = new System.Data.DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[19] { new DataColumn("رقم الطلب"),  
                                                     new DataColumn("امر الشغل"),
                                                       new DataColumn("المحافظه"),
                                                       new DataColumn("حاله الطلب"),
                                                        new DataColumn("اسم مقدم الطلب"),
                                                         new DataColumn("نوع النشاط"),
                                                         new DataColumn("تاريخ الطلب"),
                                                         new DataColumn("اسم المستخدم لطلب اعاده الدراسه"),
                                                          new DataColumn("المركز الجغرافي"),
                                                          new DataColumn("قرار مركز المتغيرات")
                                                          ,  new DataColumn("القانونيه")
                                                          , new DataColumn("ملاحظات الدراسه")
                                                          , new DataColumn("المساحه المدخله"),
                                                          new DataColumn("وحده القياس"),
                                                           new DataColumn("التظلم")
                                                           ,new DataColumn("المساحه الواقعيه"),
                                                           new DataColumn("الرد"),
                                                           new DataColumn("تاريخ الرد"),new DataColumn("حاله الرد")
                                                            
                                                    });
            var chang = ChangesCenterDescion1 == "None" ? "" : ChangesCenterDescion1;
            var fullterms = fullfilterms == "None" ? "" : fullfilterms;
            var geographic_person_responsee = geographic_person_response == "None" ? "" : geographic_person_response;
            var tazalomm = tazalom == "None" ? "" : tazalom;
            var lst = searchexcel(null, income_no, geographic_person_responsee, governate, studentUser, null, null, null, null, tazalomm,
                   null, fullterms, status, null, activity1, revieweruser, LegalFullfied, chang);
            if (lst.Count() != 0)
            {
                if (Responsestatus == "تم الرد")
                    lst = lst.Where(x => x.OrderStatus == "تم الرد والحفظ فى الارشيف" || x.ResponseApproval == "موافق").ToList();
                if (Responsestatus == "لم يتم الرد")
                    lst = lst.Where(x => x.OrderStatus != "تم الرد والحفظ فى الارشيف" || x.ResponseApproval != "موافق").ToList();

            }
            var legal = "";



            foreach (var item in lst)
            {
                legal = item.LegalFullfied == null ? "" : item.LegalFullfied.Trim();
                var gov = item.governate == null ? "" : item.governate.Trim();
                var stat = item.status == null ? "" : item.status.Trim();
                var nam = item.name == null ? "" : item.name.Trim();
                var activit = item.activity == null ? "" : item.activity.Trim();
                var uploaddat = item.uploaddate == null ? "" : item.uploaddate.Trim();
                var studentUse = item.studentUser == null ? "" : item.studentUser.Trim();
                var geographic_person_respons = item.geographic_person_response == null ? "" : item.geographic_person_response.Trim();
                var ChangesCenterDescion = item.ChangesCenterDescion == null ? "" : item.ChangesCenterDescion.Trim();
                var studynote = item.studynotes == null ? "" : item.studynotes.Trim();
                var unit1 = item.unit == null ? "" : item.unit.Trim();
                var tazalom1 = item.tazalom == null ? "" : item.tazalom.Trim();
              
                var respdate = item.responsedate == null ? "" : item.responsedate.Trim();
                var responseuser = item.responseUser == null ? "" : item.responseUser.Trim();
                var responsestatus = item.responsestatus == null ? "" : item.responsestatus.Trim();
                dt.Rows.Add(item.id_no, item.income_no.Trim(), gov, stat, nam, activit, uploaddat, studentUse,
                     geographic_person_respons, ChangesCenterDescion, legal, studynote, item.area, unit1, tazalom1,item.Convertedspace,respdate,responseuser,responsestatus);

            }

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {
                //wb.ShowRowColHeaders ;
                wb.RowHeight = 10;
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream()) //using System.IO;  
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFile.xlsx");
                }


            }
        }
    

        public ActionResult Search(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom,
            string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity1, string revieweruser, string LegalFullfied, string ChangesCenterDescion1, string Responsestatus,int?page)
        {
            int pageindex = 1;
            int pagesize = 20;
            pageindex = page.HasValue ? Convert.ToInt32(page) : pageindex;


            if (User.Identity.IsAuthenticated)
            {
                ViewBag.actioname = "Search";

                ViewBag.users = new SelectList(GetUsers().ToList());
                ViewBag.governamentlst = new SelectList(GetGovernament().ToList());
                ViewBag.incomelst = GetIncome().Select(r => new SelectListItem { Text = r, Value = r });

                ViewBag.Status1 = new SelectList(Status().ToList());
                ViewBag.activity1 = new SelectList(activitylst().ToList());
                ViewBag.studyUser1 = new SelectList(studyUser().ToList());
                ViewBag.Revieweruser1 = new SelectList(Revieweruser().ToList());
                ViewBag.LegalFullfied1 = new SelectList(LegalFullfiedlst().ToList());
                ViewBag.idnoselected = id_no;
                ViewBag.income_noselected = income_no;

                ViewBag.geographic_person_responseselected = geographic_person_response;
                ViewBag.governateselected = governate;
                ViewBag.studentUserselected = studentUser;
                ViewBag.Descion223selected = Descion223;
                ViewBag.nameselected = name;
                ViewBag.DescionQMselected = DescionQM;
                ViewBag.responsedateselected = responsedate;
                ViewBag.tazalomselected = tazalom;
                ViewBag.RaiseSurveyorsselected = RaiseSurveyors;
                ViewBag.fullfiltermsselected = fullfilterms;
                ViewBag.statusselected = status;
                ViewBag.Delayedselected = Delayed;
                ViewBag.activityselected = activity1;
                ViewBag.revieweruserselected = revieweruser;
                ViewBag.LegalFullfiedselected = LegalFullfied;
                ViewBag.ChangesCenterDescionselected = ChangesCenterDescion1;
                ViewBag.responsestatusselected = Responsestatus;




                var lst = new List<ProcedureData>();
                //if (searchh == 1)
                //{
                //    Session["Ssearch"] = 1;

                //    Session["Sincome_noselected"] = income_no;

                //    Session["Sgovernateselected"] = governate == null ? "" : governate;
                //    //Session["recoverystudentUserselected"] = studentUser;

                //    Session["Stazalomselected"] = tazalom == null ? "" : tazalom;

                //    Session["Sfullfiltermsselected"] = fullfilterms == null ? "" : fullfilterms;
                //    Session["Sstatusselected"] = status == null ? "" : status;

                //    Session["Sactivityselected"] = activity == null ? "" : activity;

                //    Session["SLegalFullfiedselected"] = LegalFullfied == null ? "" : LegalFullfied;
                //    Session["SChangesCenterDescionselected"] = ChangesCenterDescion1 == null ? "" : ChangesCenterDescion1;

                //    Session["SResponseStatus"] = Responsestatus;

                    lst = searchby2(id_no, income_no, geographic_person_response, governate, studentUser, Descion223, name, DescionQM, responsedate, tazalom,
                    RaiseSurveyors, fullfilterms, status, Delayed, activity1, revieweruser, LegalFullfied, ChangesCenterDescion1);
                    ViewBag.count = lst.Count();
                
                if (lst.Count() != 0)
                    {
                        if (Responsestatus == "تم الرد")
                            lst = lst.Where(x => x.OrderStatus == "تم الرد والحفظ فى الارشيف" || x.ResponseApproval == "موافق").ToList();
                        else if (Responsestatus == "لم يتم الرد")
                            lst = lst.Where(x => x.OrderStatus != "تم الرد والحفظ فى الارشيف" || x.ResponseApproval != "موافق").ToList();
                        else if (Responsestatus == "" || Responsestatus == null || Responsestatus == "None")
                            return View(lst.ToPagedList(pageindex, pagesize));
                    }


                    ViewBag.count = lst.Count();
                    return View(lst.ToPagedList(pageindex, pagesize));
                //}


                //else if (Session["Ssearch"] != null && Session["Ssearch"].ToString() == "1")
                //{

                //    income_no = Session["Sincome_noselected"].ToString() == "None" ? "" : Session["Sincome_noselected"].ToString();
                //    governate = Session["Sgovernateselected"].ToString() == "None" ? "" : Session["Sgovernateselected"].ToString();
                //    tazalom = Session["Stazalomselected"].ToString() == "None" ? "" : Session["Stazalomselected"].ToString();
                //    fullfilterms = Session["Sfullfiltermsselected"].ToString() == "None" ? "" : Session["Sfullfiltermsselected"].ToString();
                //    status = Session["Sstatusselected"].ToString() == "None" ? "" : Session["Sstatusselected"].ToString();
                //    activity = Session["Sactivityselected"] == null ? "" : Session["Sactivityselected"].ToString();
                //    LegalFullfied = Session["SLegalFullfiedselected"] == null ? "" : Session["SLegalFullfiedselected"].ToString();
                //    ChangesCenterDescion1 = Session["SChangesCenterDescionselected"] == null ? "" : Session["SChangesCenterDescionselected"].ToString();
                //    Responsestatus = Session["SResponseStatus"] == null ? "" : Session["SResponseStatus"].ToString();

                //    ViewBag.idnoselected = id_no;
                //    ViewBag.income_noselected = income_no == "" ? "None" : income_no;
                //    ViewBag.geographic_person_responseselected = geographic_person_response == "" ? "None" : geographic_person_response;
                //    ViewBag.governateselected = governate == "" ? "None" : governate;
                //    ViewBag.studentUserselected = studentUser == "" ? "None" : studentUser;
                //    ViewBag.Descion223selected = Descion223 == "" ? "None" : Descion223;
                //    ViewBag.nameselected = name;
                //    ViewBag.DescionQMselected = DescionQM == "" ? "None" : DescionQM;
                //    ViewBag.responsedateselected = responsedate;
                //    ViewBag.tazalomselected = tazalom == "" ? "None" : tazalom;
                //    ViewBag.RaiseSurveyorsselected = RaiseSurveyors == "" ? "None" : RaiseSurveyors;
                //    ViewBag.fullfiltermsselected = fullfilterms == "" ? "None" : fullfilterms;
                //    ViewBag.statusselected = status == "" ? "None" : status;
                //    ViewBag.Delayedselected = Delayed == "" ? "None" : Delayed;
                //    ViewBag.activityselected = activity == "" ? "None" : activity;
                //    ViewBag.revieweruserselected = revieweruser == "" ? "None" : revieweruser;
                //    ViewBag.LegalFullfiedselected = LegalFullfied == "" ? "None" : LegalFullfied;
                //    ViewBag.ChangesCenterDescionselected = ChangesCenterDescion1 == "" ? "None" : ChangesCenterDescion1;
                //    searchh = 0;
                //    lst = searchby2("", income_no, "", governate, "", "", "", "", "", tazalom,
                //     "", fullfilterms, status, "", activity, "", LegalFullfied, ChangesCenterDescion1);
                //    if (Responsestatus == "تم الرد")
                //        lst = lst.Where(x => x.OrderStatus == "تم الرد والحفظ فى الارشيف" || x.ResponseApproval == "موافق").ToList();
                //    else if (Responsestatus == "لم يتم الرد")
                //        lst = lst.Where(x => x.OrderStatus != "تم الرد والحفظ فى الارشيف" || x.ResponseApproval != "موافق").ToList();
                //    else if (Responsestatus == "" || Responsestatus == null || Responsestatus == "None")
                //        return View(lst);


                //    return View(lst);


                //}

                //else
                //{

                //    Session["Ssearch"] = null;
                //    Session["recoveryidnoselected"] = null;
                //    Session["Sincome_noselected"] = null;
                //    Session["Sgeographic_person_responseselected"] = null;
                //    Session["Sgovernateselected"] = null;
                //    Session["SstudentUserselected"] = null;
                //    Session["Stazalomselected"] = null;
                //    Session["Sfullfiltermsselected"] = null;
                //    Session["Sstatusselected"] = null;
                //    Session["Sactivityselected"] = null;

                //    Session["SLegalFullfiedselected"] = null;
                //    Session["SChangesCenterDescionselected"] = null;


                //    lst = db.TaqninMetadata.Select(s => new ProcedureData
                //    {
                //        id = s.id,
                //        id_no = s.TaqninData.id_no,
                //        name = s.TaqninData.name,
                //        status = s.TaqninData.status,
                //        activity = s.TaqninData.activity
                //        ,
                //        ChangesCenterDescion = s.ChangesCenterDescion,
                //        responsestatus = s.responsestatus,
                //        ResponseApproval = s.ResponseApproval
                //        ,
                //        income_no = s.TaqninData.income_no,
                //        uploaddate = s.uploaddate,
                //        person_upload = s.person_upload,
                //        LegalFullfied = s.LegalFullfied,
                //        tazalom = s.TaqninData.tazalom,
                //        Taqninid = s.Taqninid,
                //        geographic_person_response = s.geographic_person_response,
                //        governate = s.TaqninData.governate,
                //        OrderStatus = s.OrderStatus,
                //        studentUser = s.studentUser
                //    }).ToList();


                //}
                //ViewBag.count = lst.Count();
                //return View(lst);
            }
            return RedirectToAction("Login", "Account");
        }


        public ActionResult searchData(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var taqninmeta = db.TaqninMetadata.FirstOrDefault(x => x.id == id);
                var taqninmeta_gpr = taqninmeta.geographic_person_response;

                List<string> tt = new List<string>
            {
                "خارج", "داخل"
            };

                ViewBag.Geo_person_response = new SelectList(tt, taqninmeta_gpr);

                return View(taqninmeta);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult DeleteIncomeNo()
        {
            if (User.Identity.IsAuthenticated)
            {
                var lst = db.TaqninData.Select(i => i.income_no).Distinct().ToList();
                var viewmodellst = new List<systemdata>();
                for (int i = 0; i < lst.Count(); i++)
                {
                    var model = new systemdata();
                    model.income_no = lst[i];
                    var llst = db.TaqninMetadata.Where(x => x.TaqninData.income_no == model.income_no).ToList();
                    model.OrdersCount = llst.Count;
                    model.uploaddate = llst[0].uploaddate;
                    model.ArchiveOrders = db.TaqninMetadata.Where(y => y.TaqninData.income_no == model.income_no && y.OrderStatus == "تم الرد والحفظ فى الارشيف").ToList().Count;
                    viewmodellst.Add(model);
                }
                ViewBag.count = viewmodellst.Count();
                return View(viewmodellst);
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Delete(string income_no)
        {
            int id = 0;
            var lst = db.TaqninMetadata.Where(x => x.TaqninData.income_no == income_no && x.OrderStatus != "تم الرد والحفظ فى الارشيف").ToList();


            if (lst.Count != 0)
            {
                var Pointlst = db.Points.Where(y => y.incomeno == income_no).ToList();
                db.Points.RemoveRange(Pointlst);

                for (int i = 0; i < lst.Count(); i++)
                {

                    id = lst[i].Taqninid;
                    var model = db.TaqninData.SingleOrDefault(x => x.Taqninid == id && x.income_no == income_no);
                    db.TaqninData.Remove(model);


                }
                var log = new LogTable();
                log.userName = System.Web.HttpContext.Current.User.Identity.Name;

                log.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                log.action = " حذف امر شغل";
                db.LogTable.Add(log);
                db.TaqninMetadata.RemoveRange(lst);
                var income = db.Income_noData.Where(x => x.income_no == income_no).ToList();
                if (income.Count != 0)
                    db.Income_noData.Remove(income[0]);
                db.SaveChanges();
            }
            return RedirectToAction("DeleteIncomeNo");
        }
        public ActionResult DeleteOrders()
        {
            if (User.Identity.IsAuthenticated)
            {
                var lst = db.TaqninMetadata.Where(x => x.OrderStatus != "تم الرد والحفظ فى الارشيف").ToList();
                ViewBag.count = lst.Count();
                return View(lst);
            }
            return RedirectToAction("Login", "Account");

        }
        public ActionResult DeleteOrderr(int id)
        {
            var Taqninmetadata = db.TaqninMetadata.FirstOrDefault(y => y.Taqninid == id);
            var pointlst = db.Points.Where(z => z.idno == Taqninmetadata.TaqninData.id_no && z.incomeno == Taqninmetadata.TaqninData.income_no).ToList();

            db.Points.RemoveRange(pointlst);
            var log = new LogTable();
            var taqninmodel = db.TaqninData.FirstOrDefault(x => x.Taqninid == id);
            log.id_no = Taqninmetadata.TaqninData.id_no;
            log.Date = DateTime.Now.ToString("dd/MM/yyyy");
            db.TaqninMetadata.Remove(Taqninmetadata);
            db.TaqninData.Remove(taqninmodel);


            log.userName = System.Web.HttpContext.Current.User.Identity.Name;

            log.action = " حذف طلبات";
            db.LogTable.Add(log);

            db.SaveChanges();
            return RedirectToAction("DeleteOrders");

        }
        public ActionResult GeographicOrders()
        {
            if (User.Identity.IsAuthenticated)
            {
                var lst = db.TaqninMetadata.Where(x => x.OrderStatus == "دراسه المركز الجغرافي").ToList();
                ViewBag.count = lst.Count();
                return View(lst);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult SuspendedOrders(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom,
           string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity, int? searchh, string revieweruser, string LegalFullfied, string ChangesCenterDescion)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.actioname = "SuspendedOrders";

                ViewBag.users = new SelectList(GetUsers().ToList());
                ViewBag.governamentlst = new SelectList(GetGovernament().ToList());
                ViewBag.incomelst = new SelectList(GetIncome().ToList());
                ViewBag.Status1 = new SelectList(Status().ToList());
                ViewBag.activity1 = new SelectList(activitylst().ToList());
                ViewBag.studyUser1 = new SelectList(studyUser().ToList());
                ViewBag.Revieweruser1 = new SelectList(Revieweruser().ToList());
                ViewBag.LegalFullfied1 = new SelectList(LegalFullfiedlst().ToList());

                var lst = new List<TaqninMetadata>();
                if (searchh == 1)
                {
                    lst = searchby(id_no, income_no, geographic_person_response, governate, studentUser, Descion223, name, DescionQM, responsedate, tazalom,
                    RaiseSurveyors, fullfilterms, status, Delayed, activity, revieweruser, LegalFullfied, ChangesCenterDescion);
                    if (lst.Count != 0)
                        lst = lst.Where(x => x.SuspendedOrder == true).ToList();
                }
                else
                    lst = db.TaqninMetadata.Where(x => x.SuspendedOrder == true).ToList();
                ViewBag.count = lst.Count();
                return View(lst);

            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult SuspendedorderData(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var taqninmeta = db.TaqninMetadata.FirstOrDefault(x => x.id == id);
                var taqninmeta_gpr = taqninmeta.geographic_person_response;

                List<string> tt = new List<string>
            {
                "خارج", "داخل"
            };

                ViewBag.Geo_person_response = new SelectList(tt, taqninmeta_gpr);
                return View(taqninmeta);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult SaveSuspend(int id, string PoineerNotes, FormCollection frm)
        {

            var data = db.TaqninMetadata.FirstOrDefault(x => x.id == id);
            data.SuspendedOrder = false;
            var log = new LogTable();
            log.userName = System.Web.HttpContext.Current.User.Identity.Name;
            log.id_no = data.TaqninData.id_no;
            log.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            log.action = "ارجاع الطلب المؤجل ";

            switch (data.suspendedBy)
            {
                case "captian":
                    data.BackToCaptian = 1;
                    data.OrderStatus = "اعتماد رئيس قسم استرداد اراضي الدوله";
                    break;
                case "reviewer":

                    data.BackToReviewer = 1;
                    data.OrderStatus = "اعتماد المراجع";
                    break;

                case "variablescenter":

                    data.BackToVariableCenter = 1;
                    data.OrderStatus = "قرار مركز المتغيرات";
                    break;

                case "response":
                    data.BackToresponse = 1;
                    data.OrderStatus = "الرد";
                    break;
            }

            data.SuspendedOrder = false;
            data.suspendedBy = "";
           
            data.PoineerNotes = frm["PoineerNotes"];
            db.SaveChanges();
            db.LogTable.Add(log);
            db.SaveChanges();
            return RedirectToAction("SuspendedOrders");
        }

        public ActionResult logdata()
        {
            if (User.Identity.IsAuthenticated)
            {
                var lst = db.LogTable.OrderByDescending(x=>x.id).ToList();
                ViewBag.count = lst.Count();
                return View(lst);
            }

            return RedirectToAction("Login", "Account");
        }
        public ActionResult exceldata()
        {
            return View();
        }
        public ActionResult exportcases(DateTime? date)
        {
            if (date == null)

                return RedirectToAction("ExportListUsingEPPlus");
            else
                return RedirectToAction("ExportToExcelwithdate", new { date = date });
        }
        public FileResult ExportToExcel()
        {

            System.Data.DataTable dt = new System.Data.DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[15] { new DataColumn("رقم الطلب"),  
                                                     new DataColumn("امر الشغل"),
                                                       new DataColumn("المحافظه"),
                                                       new DataColumn("حاله الطلب"),
                                                        new DataColumn("اسم مقدم الطلب"),
                                                         new DataColumn("نوع النشاط"),
                                                         new DataColumn("تاريخ الطلب"),
                                                         new DataColumn("اسم المستخدم لطلب اعاده الدراسه"),
                                                          new DataColumn("المركز الجغرافي"),
                                                          new DataColumn("قرار مركز المتغيرات"),  new DataColumn("القانونيه"), new DataColumn("ملاحظات الدراسه"), new DataColumn("المساحه"), new DataColumn("وحده القياس"),
                                                           new DataColumn("التظلم")
                                                            
                                                    });
            var lst = db.TaqninMetadata.Where(x => x.ResponseApproval == "موافق").ToList();
            var legal = "";
            foreach (var item in lst)
            {
                legal = item.LegalFullfied == null ? "" : item.LegalFullfied.Trim();
                dt.Rows.Add(item.TaqninData.id_no, item.TaqninData.income_no.Trim(), item.TaqninData.governate.Trim(), item.TaqninData.status.Trim(), item.TaqninData.name.Trim(), item.TaqninData.activity.Trim(), item.uploaddate.Trim(), item.studentUser.Trim(),
                    item.geographic_person_response.Trim(), item.ChangesCenterDescion.Trim(), legal, item.studynotes, item.TaqninData.area, item.TaqninData.unit, item.TaqninData.tazalom.Trim());

            }

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {
                //wb.ShowRowColHeaders ;
                wb.RowHeight = 10;
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream()) //using System.IO;  
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFile.xlsx");
                }
            }
        }

        public FileResult ExportToExcelwithdate(DateTime date)
        {

            System.Data.DataTable dt = new System.Data.DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[15] { new DataColumn("رقم الطلب"),  
                                                     new DataColumn("امر الشغل"),
                                                       new DataColumn("المحافظه"),
                                                       new DataColumn("حاله الطلب"),
                                                        new DataColumn("اسم مقدم الطلب"),
                                                         new DataColumn("نوع النشاط"),
                                                         new DataColumn("تاريخ الطلب"),
                                                         new DataColumn("اسم المستخدم لطلب اعاده الدراسه"),
                                                          new DataColumn("المركز الجغرافي"),
                                                          new DataColumn("قرار مركز المتغيرات"),  new DataColumn("القانونيه"), new DataColumn("ملاحظات الدراسه"), new DataColumn("المساحه"), new DataColumn("وحده القياس"),
                                                           new DataColumn("التظلم")
                                                            
                                                    });
            var daaate = date.ToString("dd/MM/yyyy");
            var lst = db.TaqninMetadata.Where(x => x.ResponseApproval == "موافق" && x.responsedate == daaate).ToList();
            var legal = "";
            foreach (var item in lst)
            {
                legal = item.LegalFullfied == null ? "" : item.LegalFullfied.Trim();
                dt.Rows.Add(item.TaqninData.id_no, item.TaqninData.income_no.Trim(), item.TaqninData.governate.Trim(), item.TaqninData.status.Trim(), item.TaqninData.name.Trim(), item.TaqninData.activity.Trim(), item.uploaddate.Trim(), item.studentUser.Trim(),
                    item.geographic_person_response.Trim(), item.ChangesCenterDescion.Trim(), legal, item.studynotes, item.TaqninData.area, item.TaqninData.unit, item.TaqninData.tazalom.Trim());

            }

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {
                //wb.ShowRowColHeaders ;
                wb.RowHeight = 10;
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream()) //using System.IO;  
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFile.xlsx");
                }
            }
        }



    
        public List<TaqninMetadata> searchby(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom,
            string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity, string revieweruser, string LegalFullfied, string ChangesCenterDescion)
        {
            bool flag = false;
            var lst = db.TaqninMetadata.AsNoTracking().ToList();
            var countt = lst.Count();
            if (!String.IsNullOrEmpty(id_no))
            {
                flag = true;
                lst = lst.Where(x => x.TaqninData.id_no.Contains(id_no.Trim())).ToList();
            } if (!String.IsNullOrEmpty(income_no))
            {
                flag = true;
                lst = lst.Where(x => x.TaqninData.income_no.Contains(income_no.Trim())).ToList();
            }
            if (!String.IsNullOrEmpty(geographic_person_response) && geographic_person_response != "None")
            {
                lst = lst.Where(x => x.geographic_person_response == (geographic_person_response)).ToList();
                flag = true;
            }
            if (!String.IsNullOrEmpty(governate) && governate != "None")
            {
                lst = lst.Where(x => x.TaqninData.governate == (governate)).ToList();
                flag = true;
            } if (!String.IsNullOrEmpty(studentUser) && studentUser != "None")
            {
                flag = true;
                lst = lst.Where(x => x.studentUser == studentUser).ToList();
            }
            if (!String.IsNullOrEmpty(name))
            {
                flag = true;
                lst = lst.Where(x => x.TaqninData.name.Contains(name.Trim())).ToList();
            }
            if (!String.IsNullOrEmpty(Descion223) && Descion223 != "None")
            {
                flag = true;
                lst = lst.Where(x => x.Descion223 == Descion223).ToList();
            }
            if (!String.IsNullOrEmpty(DescionQM) && DescionQM != "None")
            {
                flag = true;
                lst = lst.Where(x => x.DescionQM == DescionQM).ToList();
            }
            if (!String.IsNullOrEmpty(responsedate))
            {
                flag = true;
                lst = lst.Where(x => x.responsedate == responsedate).ToList();
            }


            if (!String.IsNullOrEmpty(tazalom) && tazalom != "None")
            {
                flag = true;
                lst = lst.Where(x => x.TaqninData.tazalom == tazalom).ToList();
            }
            if (!String.IsNullOrEmpty(RaiseSurveyors) && RaiseSurveyors != "None")
            {
                flag = true;
                lst = lst.Where(x => x.RaiseSurveyors == RaiseSurveyors).ToList();
            }

            if (!String.IsNullOrEmpty(fullfilterms) && fullfilterms != "None")
            {
                flag = true;
                lst = lst.Where(x => x.AreaFullfied == fullfilterms).ToList();
            }
            if (!String.IsNullOrEmpty(status) && status != "None")
            {
                flag = true;
                lst = lst.Where(x => x.TaqninData.status == status).ToList();
            }
            if (!String.IsNullOrEmpty(activity) && activity != "None")
            {
                flag = true;
                lst = lst.Where(x => x.TaqninData.activity == activity).ToList();
            }
            if (!String.IsNullOrEmpty(Delayed) && Delayed != "None")
            {
                flag = true;
                var search = Delayed == "مؤجله" ? true : false;

                lst = lst.Where(x => x.SuspendedOrder == search).ToList();
            }
            if (!String.IsNullOrEmpty(ChangesCenterDescion) && ChangesCenterDescion != "None")
            {
                flag = true;
                lst = lst.Where(x => x.ChangesCenterDescion == ChangesCenterDescion).ToList();
            }
            if (!String.IsNullOrEmpty(LegalFullfied) && LegalFullfied != "None")
            {
                flag = true;


                lst = lst.Where(x => x.LegalFullfied == LegalFullfied).ToList();
            }
            if (lst.Count == 0 || (lst.Count == countt) && flag == true)
            {
                lst = new List<TaqninMetadata>();
                return lst;
            }
            return lst;
        }

        public List<ProcedureData> searchby2(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom,
           string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity, string revieweruser, string LegalFullfied, string ChangesCenterDescion)
        {


            //var param_1 = geographic_person_response == "None" ? "" : geographic_person_response;
            //var param_2 = Descion223 == "None" ? "" : Descion223;
            //var param_3 = DescionQM == "None" ? "" : DescionQM;
            //var param_4 = responsedate == null ? "" : responsedate;
            //var param_5 = tazalom == "None" ? "" : tazalom;
            //var param_6 = RaiseSurveyors == "None" ? "" : RaiseSurveyors;
            //var param_7 = fullfilterms == "None" ? "" : fullfilterms;
            //var param_8 = Delayed == "None" ? "" : Delayed;
            //var param_9 = ChangesCenterDescion == "None" ? "" : ChangesCenterDescion;

            //var lst = db.Database.SqlQuery<ProcedureData>("EXECUTE  [dbo].[SearchinDB]  @id_no,@income_no ,@geographic_person_response,@governate ,@studentUser ,@Descion223,@name,@DescionQM,@responsedate,@tazalom,@RaiseSurveyors,@fullfilterms,@status,@Delayed,@activity,@LegalFullfied,@ChangesCenterDescion",
            //    new SqlParameter("@id_no", id_no),
            //    new SqlParameter("@income_no", income_no),
            //    new SqlParameter("@geographic_person_response", param_1),
            //    new SqlParameter("@governate", governate),
            //    new SqlParameter("@studentUser", studentUser),
            //    new SqlParameter("@Descion223", param_2),
            //    new SqlParameter("@name", name),
            //    new SqlParameter("@DescionQM", param_3),
            //    new SqlParameter("@responsedate", param_4),
            //    new SqlParameter("@tazalom", param_5),
            //    new SqlParameter("@RaiseSurveyors", param_6),
            //    new SqlParameter("@fullfilterms", param_7),
            //    new SqlParameter("@status", status),
            //    new SqlParameter("@Delayed", param_8),
            //    new SqlParameter("@activity", activity),
            //    new SqlParameter("@LegalFullfied", LegalFullfied),
            //    new SqlParameter("@ChangesCenterDescion", param_9)
            //    ).ToList();

            //return lst;
            var param_1 = geographic_person_response == "None" || geographic_person_response == null ? "" : geographic_person_response;
            var param_2 = Descion223 == "None" || Descion223 == null ? "" : Descion223;
            var param_3 = DescionQM == "None" || DescionQM == null ? "" : DescionQM;
            var param_4 = responsedate == null ? "" : responsedate;
            var param_5 = tazalom == "None" || tazalom == null ? "" : tazalom;
            var param_6 = RaiseSurveyors == "None" || RaiseSurveyors == null ? "" : RaiseSurveyors;
            var param_7 = fullfilterms == "None" || fullfilterms == null ? "" : fullfilterms;
            var param_8 = Delayed == "None" || Delayed == null ? "" : Delayed;
            var param_9 = ChangesCenterDescion == "None" || ChangesCenterDescion == null ? "" : ChangesCenterDescion;
            var param_10 = income_no == null ? "" : income_no;
            var param_11 = studentUser == null ? "" : studentUser;
            var param_12 = status == null ? "" : status;
            var param_13 = activity == null ? "" : activity;
            var param_14 = LegalFullfied == null ? "" : LegalFullfied;
            var param_15 = governate == null ? "" : governate;
            var param_16 = id_no == null ? "" : id_no;
            var param_17 = name == null ? "" : name;
            var lst = db.Database.SqlQuery<ProcedureData>("EXECUTE  [dbo].[SearchinDB]  @id_no,@income_no ,@geographic_person_response,@governate ,@studentUser ,@Descion223,@name,@DescionQM,@responsedate,@tazalom,@RaiseSurveyors,@fullfilterms,@status,@Delayed,@activity,@LegalFullfied,@ChangesCenterDescion",
                new SqlParameter("@id_no", param_16),
                new SqlParameter("@income_no", param_10),
                new SqlParameter("@geographic_person_response", param_1),
                new SqlParameter("@governate", param_15),
                new SqlParameter("@studentUser", param_11),
                new SqlParameter("@Descion223", param_2),
                new SqlParameter("@name", param_17),
                new SqlParameter("@DescionQM", param_3),
                new SqlParameter("@responsedate", param_4),
                new SqlParameter("@tazalom", param_5),
                new SqlParameter("@RaiseSurveyors", param_6),
                new SqlParameter("@fullfilterms", param_7),
                new SqlParameter("@status", param_12),
                new SqlParameter("@Delayed", param_8),
                new SqlParameter("@activity", param_13),
                new SqlParameter("@LegalFullfied", param_14),
                new SqlParameter("@ChangesCenterDescion", param_9)
                ).ToList();
            return lst;
        }


        public List<exceldatadb> searchexcel(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom,
          string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity, string revieweruser, string LegalFullfied, string ChangesCenterDescion)
        {
            var param_1 = geographic_person_response == "None" || geographic_person_response == null ? "" : geographic_person_response;
            var param_2 = Descion223 == "None" || Descion223 == null ? "" : Descion223;
            var param_3 = DescionQM == "None" || DescionQM == null ? "" : DescionQM;
            var param_4 = responsedate == null ? "" : responsedate;
            var param_5 = tazalom == "None" || tazalom == null ? "" : tazalom;
            var param_6 = RaiseSurveyors == "None" || RaiseSurveyors == null ? "" : RaiseSurveyors;
            var param_7 = fullfilterms == "None" || fullfilterms == null ? "" : fullfilterms;
            var param_8 = Delayed == "None" || Delayed == null ? "" : Delayed;
            var param_9 = ChangesCenterDescion == "None" || ChangesCenterDescion == null ? "" : ChangesCenterDescion;
            var param_10 = income_no == null ? "" : income_no;
            var param_11 = studentUser == null ? "" : studentUser;
            var param_12 = status == null ? "" : status;
            var param_13 = activity == null ? "" : activity;
            var param_14 = LegalFullfied == null ? "" : LegalFullfied;
            var param_15 = governate == null ? "" : governate;
            var param_16 = id_no == null ? "" : id_no;
            var param_17 = name == null ? "" : name;

            var lst = db.Database.SqlQuery<exceldatadb>("EXECUTE  [dbo].[SearchinDB]  @id_no,@income_no ,@geographic_person_response,@governate ,@studentUser ,@Descion223,@name,@DescionQM,@responsedate,@tazalom,@RaiseSurveyors,@fullfilterms,@status,@Delayed,@activity,@LegalFullfied,@ChangesCenterDescion",
                new SqlParameter("@id_no", param_16),
                new SqlParameter("@income_no", param_10),
                new SqlParameter("@geographic_person_response", param_1),
                new SqlParameter("@governate", param_15),
                new SqlParameter("@studentUser", param_11),
                new SqlParameter("@Descion223", param_2),
                new SqlParameter("@name", param_17),
                new SqlParameter("@DescionQM", param_3),
                new SqlParameter("@responsedate", param_4),
                new SqlParameter("@tazalom", param_5),
                new SqlParameter("@RaiseSurveyors", param_6),
                new SqlParameter("@fullfilterms", param_7),
                new SqlParameter("@status", param_12),
                new SqlParameter("@Delayed", param_8),
                new SqlParameter("@activity", param_13),
                new SqlParameter("@LegalFullfied", param_14),
                new SqlParameter("@ChangesCenterDescion", param_9)
               
                ).ToList();
            return lst;
        }

        public List<ProcedureData> searchby4(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom,
           string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity, string revieweruser, string LegalFullfied, string ChangesCenterDescion)
        {


            var param_1 = geographic_person_response == "None" || geographic_person_response == null ? "" : geographic_person_response;
            var param_2 = Descion223 == "None" || Descion223 == null ? "" : Descion223;
            var param_3 = DescionQM == "None" || DescionQM == null ? "" : DescionQM;
            var param_4 = responsedate == null ? "" : responsedate;
            var param_5 = tazalom == "None" || tazalom == null ? "" : tazalom;
            var param_6 = RaiseSurveyors == "None" || RaiseSurveyors == null ? "" : RaiseSurveyors;
            var param_7 = fullfilterms == "None" || fullfilterms == null ? "" : fullfilterms;
            var param_8 = Delayed == "None" || Delayed == null ? "" : Delayed;
            var param_9 = ChangesCenterDescion == "None" || ChangesCenterDescion == null ? "" : ChangesCenterDescion;
            var param_10 = income_no == null ? "" : income_no;
            var param_11 = studentUser == null ? "" : studentUser;
            var param_12 = status == null ? "" : status;
            var param_13 = activity == null ? "" : activity;
            var param_14 = LegalFullfied == null ? "" : LegalFullfied;
            var param_15 = governate == null ? "" : governate;
            var param_16 = id_no == null ? "" : id_no;
            var param_17 = name == null ? "" : name;
            var lst = db.Database.SqlQuery<ProcedureData>("EXECUTE  [dbo].[ArchiveSearch]  @id_no,@income_no ,@geographic_person_response,@governate ,@studentUser ,@Descion223,@name,@DescionQM,@responsedate,@tazalom,@RaiseSurveyors,@fullfilterms,@status,@Delayed,@activity,@LegalFullfied,@ChangesCenterDescion",
                new SqlParameter("@id_no", param_16),
                new SqlParameter("@income_no", param_10),
                new SqlParameter("@geographic_person_response", param_1),
                new SqlParameter("@governate", param_15),
                new SqlParameter("@studentUser", param_11),
                new SqlParameter("@Descion223", param_2),
                new SqlParameter("@name", param_17),
                new SqlParameter("@DescionQM", param_3),
                new SqlParameter("@responsedate", param_4),
                new SqlParameter("@tazalom", param_5),
                new SqlParameter("@RaiseSurveyors", param_6),
                new SqlParameter("@fullfilterms", param_7),
                new SqlParameter("@status", param_12),
                new SqlParameter("@Delayed", param_8),
                new SqlParameter("@activity", param_13),
                new SqlParameter("@LegalFullfied", param_14),
                new SqlParameter("@ChangesCenterDescion", param_9)
                ).ToList();

            return lst;
        }



        public List<CaptianData> searchby3(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom,
                 string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity, string revieweruser, string LegalFullfied, string ChangesCenterDescion)
        {


            var param_1 = geographic_person_response == "None" || geographic_person_response == null ? "" : geographic_person_response;
            var param_2 = Descion223 == "None" || Descion223 == null ? "" : Descion223;
            var param_3 = DescionQM == "None" || DescionQM == null ? "" : DescionQM;
            var param_4 = responsedate == null ? "" : responsedate;
            var param_5 = tazalom == "None" || tazalom == null ? "" : tazalom;
            var param_6 = RaiseSurveyors == "None" || RaiseSurveyors == null ? "" : RaiseSurveyors;
            var param_7 = fullfilterms == "None" || fullfilterms == null ? "" : fullfilterms;
            var param_8 = Delayed == "None" || Delayed == null ? "" : Delayed;
            var param_9 = ChangesCenterDescion == "None" || ChangesCenterDescion == null ? "" : ChangesCenterDescion;
            var param_10 = income_no == null ? "" : income_no;
            var param_11 = studentUser == null ? "" : studentUser;
            var param_12 = status == null ? "" : status;
            var param_13 = activity == null ? "" : activity;
            var param_14 = LegalFullfied == null ? "" : LegalFullfied;
            var param_15 = governate == null ? "" : governate;
            var param_16 = id_no == null ? "" : id_no;
            var param_17 = name == null ? "" : name;
            var lst = db.Database.SqlQuery<CaptianData>("EXECUTE  [dbo].[CaptianProcedure]  @id_no,@income_no ,@geographic_person_response,@governate ,@studentUser ,@Descion223,@name,@DescionQM,@responsedate,@tazalom,@RaiseSurveyors,@fullfilterms,@status,@Delayed,@activity,@LegalFullfied,@ChangesCenterDescion",
                new SqlParameter("@id_no", param_16),
                new SqlParameter("@income_no", param_10),
                new SqlParameter("@geographic_person_response", param_1),
                new SqlParameter("@governate", param_15),
                new SqlParameter("@studentUser", param_11),
                new SqlParameter("@Descion223", param_2),
                new SqlParameter("@name", param_17),
                new SqlParameter("@DescionQM", param_3),
                new SqlParameter("@responsedate", param_4),
                new SqlParameter("@tazalom", param_5),
                new SqlParameter("@RaiseSurveyors", param_6),
                new SqlParameter("@fullfilterms", param_7),
                new SqlParameter("@status", param_12),
                new SqlParameter("@Delayed", param_8),
                new SqlParameter("@activity", param_13),
                new SqlParameter("@LegalFullfied", param_14),
                new SqlParameter("@ChangesCenterDescion", param_9)
                ).ToList();

            return lst;
        }

        public List<TaqninMetadata> searchby1(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom,
    string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity, string revieweruser, string LegalFullfied, string ChangesCenterDescion)
        {
            bool flag = false;
            var lst = db.TaqninMetadata.ToList();
            var countt = lst.Count();
            if (!String.IsNullOrEmpty(id_no))
            {
                flag = true;
                lst = lst.Where(x => x.TaqninData.id_no.Contains(id_no.Trim())).ToList();
            } if (!String.IsNullOrEmpty(income_no))
            {
                flag = true;
                lst = lst.Where(x => x.TaqninData.income_no.Contains(income_no.Trim())).ToList();
            }
            if (!String.IsNullOrEmpty(geographic_person_response) && geographic_person_response != "None")
            {
                lst = lst.Where(x => x.geographic_person_response == (geographic_person_response)).ToList();
                flag = true;
            }
            if (!String.IsNullOrEmpty(governate) && governate != "None")
            {
                lst = lst.Where(x => x.TaqninData.governate == (governate)).ToList();
                flag = true;
            } if (!String.IsNullOrEmpty(studentUser) && studentUser != "None")
            {
                flag = true;
                lst = lst.Where(x => x.studentUser == studentUser).ToList();
            }
            if (!String.IsNullOrEmpty(name))
            {
                flag = true;
                lst = lst.Where(x => x.TaqninData.name.Contains(name.Trim())).ToList();
            }
            if (!String.IsNullOrEmpty(Descion223) && Descion223 != "None")
            {
                flag = true;
                lst = lst.Where(x => x.Descion223 == Descion223).ToList();
            }
            if (!String.IsNullOrEmpty(DescionQM) && DescionQM != "None")
            {
                flag = true;
                lst = lst.Where(x => x.DescionQM == DescionQM).ToList();
            }
            if (!String.IsNullOrEmpty(responsedate))
            {
                flag = true;
                lst = lst.Where(x => x.responsedate == responsedate).ToList();
            }


            if (!String.IsNullOrEmpty(tazalom) && tazalom != "None")
            {
                flag = true;
                lst = lst.Where(x => x.TaqninData.tazalom == tazalom).ToList();
            }
            if (!String.IsNullOrEmpty(RaiseSurveyors) && RaiseSurveyors != "None")
            {
                flag = true;
                lst = lst.Where(x => x.RaiseSurveyors == RaiseSurveyors).ToList();
            }

            if (!String.IsNullOrEmpty(fullfilterms) && fullfilterms != "None")
            {
                flag = true;
                lst = lst.Where(x => x.AreaFullfied == fullfilterms).ToList();
            }
            if (!String.IsNullOrEmpty(status) && status != "None")
            {
                flag = true;
                lst = lst.Where(x => x.TaqninData.status == status).ToList();
            }
            if (!String.IsNullOrEmpty(activity) && activity != "None")
            {
                flag = true;
                lst = lst.Where(x => x.TaqninData.activity == activity).ToList();
            }
            if (!String.IsNullOrEmpty(Delayed) && Delayed != "None")
            {
                flag = true;
                var search = Delayed == "مؤجله" ? true : false;

                lst = lst.Where(x => x.SuspendedOrder == search).ToList();
            }
            if (!String.IsNullOrEmpty(ChangesCenterDescion) && ChangesCenterDescion != "None")
            {
                flag = true;
                lst = lst.Where(x => x.ChangesCenterDescion == ChangesCenterDescion).ToList();
            }
            if (!String.IsNullOrEmpty(LegalFullfied) && LegalFullfied != "None")
            {
                flag = true;


                lst = lst.Where(x => x.LegalFullfied == LegalFullfied).ToList();
            }
            return lst;
        }

        public List<CaptianData> searchbyPoineer(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom,
      string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity, string revieweruser, string LegalFullfied, string ChangesCenterDescion)
        {


            var param_1 = geographic_person_response == "None" || geographic_person_response == null ? "" : geographic_person_response;
            var param_2 = Descion223 == "None" || Descion223 == null ? "" : Descion223;
            var param_3 = DescionQM == "None" || DescionQM == null ? "" : DescionQM;
            var param_4 = responsedate == null ? "" : responsedate;
            var param_5 = tazalom == "None" || tazalom == null ? "" : tazalom;
            var param_6 = RaiseSurveyors == "None" || RaiseSurveyors == null ? "" : RaiseSurveyors;
            var param_7 = fullfilterms == "None" || fullfilterms == null ? "" : fullfilterms;
            var param_8 = Delayed == "None" || Delayed == null ? "" : Delayed;
            var param_9 = ChangesCenterDescion == "None" || ChangesCenterDescion == null ? "" : ChangesCenterDescion;
            var param_10 = income_no == null ? "" : income_no;
            var param_11 = studentUser == null ? "" : studentUser;
            var param_12 = status == null ? "" : status;
            var param_13 = activity == null ? "" : activity;
            var param_14 = LegalFullfied == null ? "" : LegalFullfied;
            var param_15 = governate == null ? "" : governate;
            var param_16 = id_no == null ? "" : id_no;
            var param_17 = name == null ? "" : name;
            var lst = db.Database.SqlQuery<CaptianData>("EXECUTE  [dbo].[PoineerProcedure]  @id_no,@income_no ,@geographic_person_response,@governate ,@studentUser ,@Descion223,@name,@DescionQM,@responsedate,@tazalom,@RaiseSurveyors,@fullfilterms,@status,@Delayed,@activity,@LegalFullfied,@ChangesCenterDescion",
                new SqlParameter("@id_no", param_16),
                new SqlParameter("@income_no", param_10),
                new SqlParameter("@geographic_person_response", param_1),
                new SqlParameter("@governate", param_15),
                new SqlParameter("@studentUser", param_11),
                new SqlParameter("@Descion223", param_2),
                new SqlParameter("@name", param_17),
                new SqlParameter("@DescionQM", param_3),
                new SqlParameter("@responsedate", param_4),
                new SqlParameter("@tazalom", param_5),
                new SqlParameter("@RaiseSurveyors", param_6),
                new SqlParameter("@fullfilterms", param_7),
                new SqlParameter("@status", param_12),
                new SqlParameter("@Delayed", param_8),
                new SqlParameter("@activity", param_13),
                new SqlParameter("@LegalFullfied", param_14),
                new SqlParameter("@ChangesCenterDescion", param_9)
                ).ToList();

            return lst;
        }
      
        public List<CaptianData> searchbymajor(string id_no, string income_no, string governate, string studentUser)
        {


           var param_10 = income_no == null ? "" : income_no;
            var param_11 = studentUser == null ? "" : studentUser;
          
            var param_15 = governate == null ? "" : governate;
            var param_16 = id_no == null ? "" : id_no;
           
            var lst = db.Database.SqlQuery<CaptianData>("EXECUTE  [dbo].[majorProcedure]  @id_no,@income_no ,@governate ,@studentUser" ,
                new SqlParameter("@id_no", param_16),
                new SqlParameter("@income_no", param_10),
              
                new SqlParameter("@governate", param_15),
                new SqlParameter("@studentUser", param_11)
              
              
            
                ).ToList();

            return lst;
        }

        public List<ResponseData> searchby5(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom,
        string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity, string revieweruser, string LegalFullfied, string ChangesCenterDescion)
        {


            var param_1 = geographic_person_response == "None" || geographic_person_response == null ? "" : geographic_person_response;
            var param_2 = Descion223 == "None" || Descion223 == null ? "" : Descion223;
            var param_3 = DescionQM == "None" || DescionQM == null ? "" : DescionQM;
            var param_4 = responsedate == null  ? "" : responsedate;
            var param_5 = tazalom == "None" || tazalom == null ? "" : tazalom;
            var param_6 = RaiseSurveyors == "None" || RaiseSurveyors == null ? "" : RaiseSurveyors;
            var param_7 = fullfilterms == "None" || fullfilterms == null ? "" : fullfilterms;
            var param_8 = Delayed == "None" || Delayed == null ? "" : Delayed;
            var param_9 = ChangesCenterDescion == "None" || ChangesCenterDescion == null ? "" : ChangesCenterDescion;
            var param_10 = income_no == null ? "" : income_no;
            var param_11 = studentUser == null ? "" : studentUser;
            var param_12 = status == null ? "" : status;
            var param_13 = activity == null ? "" : activity;
            var param_14 = LegalFullfied == null ? "" : LegalFullfied;
            var param_15 = governate == null ? "" : governate;
            var param_16 = id_no == null ? "" : id_no;
            var param_17 = name == null ? "" : name;
            var lst = db.Database.SqlQuery<ResponseData>("EXECUTE  [dbo].[ResponseProcedure]  @id_no,@income_no ,@geographic_person_response,@governate ,@studentUser ,@Descion223,@name,@DescionQM,@responsedate,@tazalom,@RaiseSurveyors,@fullfilterms,@status,@Delayed,@activity,@LegalFullfied,@ChangesCenterDescion",
                new SqlParameter("@id_no", param_16),
                new SqlParameter("@income_no", param_10),
                new SqlParameter("@geographic_person_response", param_1),
                new SqlParameter("@governate", param_15),
                new SqlParameter("@studentUser", param_11),
                new SqlParameter("@Descion223", param_2),
                new SqlParameter("@name", param_17),
                new SqlParameter("@DescionQM", param_3),
                new SqlParameter("@responsedate", param_4),
                new SqlParameter("@tazalom", param_5),
                new SqlParameter("@RaiseSurveyors", param_6),
                new SqlParameter("@fullfilterms", param_7),
                new SqlParameter("@status", param_12),
                new SqlParameter("@Delayed", param_8),
                new SqlParameter("@activity", param_13),
                new SqlParameter("@LegalFullfied", param_14),
                new SqlParameter("@ChangesCenterDescion", param_9)
                ).ToList();

            return lst;
        }

        public List<refrenceapprovaldata> searchbyrefrenceapproval(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom,
      string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity, string revieweruser, string LegalFullfied, string ChangesCenterDescion)
        {


            var param_1 = geographic_person_response == "None" || geographic_person_response == null ? "" : geographic_person_response;
            var param_2 = Descion223 == "None" || Descion223 == null ? "" : Descion223;
            var param_3 = DescionQM == "None" || DescionQM == null ? "" : DescionQM;
            var param_4 = responsedate == null ? "" : responsedate;
            var param_5 = tazalom == "None" || tazalom == null ? "" : tazalom;
            var param_6 = RaiseSurveyors == "None" || RaiseSurveyors == null ? "" : RaiseSurveyors;
            var param_7 = fullfilterms == "None" || fullfilterms == null ? "" : fullfilterms;
            var param_8 = Delayed == "None" || Delayed == null ? "" : Delayed;
            var param_9 = ChangesCenterDescion == "None" || ChangesCenterDescion == null ? "" : ChangesCenterDescion;
            var param_10 = income_no == null ? "" : income_no;
            var param_11 = studentUser == null ? "" : studentUser;
            var param_12 = status == null ? "" : status;
            var param_13 = activity == null ? "" : activity;
            var param_14 = LegalFullfied == null ? "" : LegalFullfied;
            var param_15 = governate == null ? "" : governate;
            var param_16 = id_no == null ? "" : id_no;
            var param_17 = name == null ? "" : name;
            var lst = db.Database.SqlQuery<refrenceapprovaldata>("EXECUTE  [dbo].[RefrenceApprovalProcedure]  @id_no,@income_no ,@geographic_person_response,@governate ,@studentUser ,@Descion223,@name,@DescionQM,@responsedate,@tazalom,@RaiseSurveyors,@fullfilterms,@status,@Delayed,@activity,@LegalFullfied,@ChangesCenterDescion",
                new SqlParameter("@id_no", param_16),
                new SqlParameter("@income_no", param_10),
                new SqlParameter("@geographic_person_response", param_1),
                new SqlParameter("@governate", param_15),
                new SqlParameter("@studentUser", param_11),
                new SqlParameter("@Descion223", param_2),
                new SqlParameter("@name", param_17),
                new SqlParameter("@DescionQM", param_3),
                new SqlParameter("@responsedate", param_4),
                new SqlParameter("@tazalom", param_5),
                new SqlParameter("@RaiseSurveyors", param_6),
                new SqlParameter("@fullfilterms", param_7),
                new SqlParameter("@status", param_12),
                new SqlParameter("@Delayed", param_8),
                new SqlParameter("@activity", param_13),
                new SqlParameter("@LegalFullfied", param_14),
                new SqlParameter("@ChangesCenterDescion", param_9)
                ).ToList();

            return lst;
        }
  public List<StudyData> searchbystudy(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom,
      string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity, string revieweruser, string LegalFullfied, string ChangesCenterDescion)
        {


            var param_1 = geographic_person_response == "None" || geographic_person_response == null ? "" : geographic_person_response;
            var param_2 = Descion223 == "None" || Descion223 == null ? "" : Descion223;
            var param_3 = DescionQM == "None" || DescionQM == null ? "" : DescionQM;
            var param_4 = responsedate == null ? "" : responsedate;
            var param_5 = tazalom == "None" || tazalom == null ? "" : tazalom;
            var param_6 = RaiseSurveyors == "None" || RaiseSurveyors == null ? "" : RaiseSurveyors;
            var param_7 = fullfilterms == "None" || fullfilterms == null ? "" : fullfilterms;
            var param_8 = Delayed == "None" || Delayed == null ? "" : Delayed;
            var param_9 = ChangesCenterDescion == "None" || ChangesCenterDescion == null ? "" : ChangesCenterDescion;
            var param_10 = income_no == null ? "" : income_no;
            var param_11 = studentUser == null ? "" : studentUser;
            var param_12 = status == null ? "" : status;
            var param_13 = activity == null ? "" : activity;
            var param_14 = LegalFullfied == null ? "" : LegalFullfied;
            var param_15 = governate == null ? "" : governate;
            var param_16 = id_no == null ? "" : id_no;
            var param_17 = name == null ? "" : name;
            var lst = db.Database.SqlQuery<StudyData>("EXECUTE  [dbo].[StudyProcedure]  @id_no,@income_no ,@geographic_person_response,@governate ,@studentUser ,@Descion223,@name,@DescionQM,@responsedate,@tazalom,@RaiseSurveyors,@fullfilterms,@status,@Delayed,@activity,@LegalFullfied,@ChangesCenterDescion",
                new SqlParameter("@id_no", param_16),
                new SqlParameter("@income_no", param_10),
                new SqlParameter("@geographic_person_response", param_1),
                new SqlParameter("@governate", param_15),
                new SqlParameter("@studentUser", param_11),
                new SqlParameter("@Descion223", param_2),
                new SqlParameter("@name", param_17),
                new SqlParameter("@DescionQM", param_3),
                new SqlParameter("@responsedate", param_4),
                new SqlParameter("@tazalom", param_5),
                new SqlParameter("@RaiseSurveyors", param_6),
                new SqlParameter("@fullfilterms", param_7),
                new SqlParameter("@status", param_12),
                new SqlParameter("@Delayed", param_8),
                new SqlParameter("@activity", param_13),
                new SqlParameter("@LegalFullfied", param_14),
                new SqlParameter("@ChangesCenterDescion", param_9)
                ).ToList();

            return lst;
        }
        public List<TaqninMetadata> searchby(string id_no, string income_no, string governate, string studentUser)
        {
            bool flag = false;
            var lst = db.TaqninMetadata.ToList();
            var countt = lst.Count();

            if (!String.IsNullOrEmpty(id_no))
            {
                lst = lst.Where(x => x.TaqninData.id_no.Contains(id_no.Trim())).ToList();
            } if (!String.IsNullOrEmpty(income_no))
            {
                flag = true;
                lst = lst.Where(x => x.TaqninData.income_no.Contains(income_no.Trim())).ToList();
            }

            if (!String.IsNullOrEmpty(governate) && governate != "None")
            {
                lst = lst.Where(x => x.TaqninData.governate == (governate)).ToList();
                flag = true;
            } if (!String.IsNullOrEmpty(studentUser) && studentUser != "None")
            {
                flag = true;
                lst = lst.Where(x => x.studentUser == studentUser).ToList();
            }

            if (lst.Count == 0 || (lst.Count == countt) && flag == false)
                lst = new List<TaqninMetadata>();
            return lst;
        }
        public List<string> GetUsers()
        {
            var UsersLst = db.Users.Select(x => x.UserName).Distinct().ToList();
            return UsersLst;

        }

        public List<string> GetGovernament()
        {
            var Governament = db.TaqninData.Select(x => x.governate).Distinct().ToList();
            return Governament;

        }
        public List<string> GetIncome()
        {
            var IncomeLst = db.TaqninData.Select(x => x.income_no).Distinct().ToList();
            return IncomeLst;

        }
        public List<string> Status()
        {
            var IncomeLst = db.TaqninData.Select(x => x.status).Where(y => !String.IsNullOrEmpty(y)).Distinct().ToList();
            return IncomeLst;

        }
        public List<string> studyUser()
        {
            var IncomeLst = db.TaqninMetadata.Select(x => x.studentUser).Where(y => !String.IsNullOrEmpty(y)).Distinct().ToList();
            return IncomeLst;

        }
        public List<string> Revieweruser()
        {
            var IncomeLst = db.TaqninMetadata.Select(x => x.Reviewer).Where(y => !String.IsNullOrEmpty(y)).Distinct().ToList();
            return IncomeLst;

        }
        public List<string> LegalFullfiedlst()
        {
            var IncomeLst = db.TaqninMetadata.Select(x => x.LegalFullfied).Where(y => !String.IsNullOrEmpty(y)).Distinct().ToList();
            return IncomeLst;

        }

        public List<string> activitylst()
        {
            var IncomeLst = db.TaqninData.Select(x => x.activity).Where(y => !String.IsNullOrEmpty(y)).Distinct().ToList();
            return IncomeLst;

        }
        public void AddACfile(TaqninMetadata taqninmeta)
        {

            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
        @"Data source= D:\New folder (4)\Database21.accdb";


            conn.Open();

            var idno = taqninmeta.TaqninData.id_no;
            var name = taqninmeta.TaqninData.name;
            var activity = taqninmeta.TaqninData.activity;
            var governate = taqninmeta.TaqninData.governate;
            var unit = taqninmeta.TaqninData.unit;
            var area = taqninmeta.TaqninData.area;
            var w_man = taqninmeta.TaqninData.w_man;
            var tazalom = taqninmeta.TaqninData.tazalom;
            var study_note = taqninmeta.TaqninData.study_note;
            var income_no = taqninmeta.TaqninData.income_no;
            var status = taqninmeta.TaqninData.status;
            var shapearea = taqninmeta.TaqninData.shapearea;
            var actualarea = taqninmeta.TaqninData.actualarea;
            var geographic_person_response = taqninmeta.geographic_person_response;
            var ChangesCenterDescion = taqninmeta.ChangesCenterDescion;
            var honesty = taqninmeta.honesty;
            var RaiseSurveyors = taqninmeta.RaiseSurveyors;
            var domain = taqninmeta.Domain;
            var descion223 = taqninmeta.Descion223;
            var descionqm = taqninmeta.DescionQM;

            var lst = db.Points.Where(z => z.idno == taqninmeta.TaqninData.id_no && z.incomeno == taqninmeta.TaqninData.income_no).ToList();
            for (int i = 0; i < lst.Count; i++)
            {
                string command = "insert into TaqninDatas VALUES('" + idno + "', '" + name + "','" + activity + "','" + governate + "','" + unit + "','" + area + "','" + w_man + "','" + tazalom + "','" + study_note + "','" + income_no + "','" + status + "','" + shapearea + "','" + actualarea + "','" + geographic_person_response + "','" + ChangesCenterDescion + "','" + lst[i].x + "','" + lst[i].y + "','" + honesty + "','" + RaiseSurveyors + "','" + descion223 + "','" + descionqm + "','" + domain + "','" + lst[i].id + "')";
                OleDbCommand cmd = new OleDbCommand(command, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddACfile1()
        {

            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
        @"Data source= F:\Data\backup.mdb";


            conn.Open();
            var lstt = db.TaqninMetadata.ToList();
            foreach (var taqninmeta in lstt)
            {
                var idno = taqninmeta.TaqninData.id_no;
                var name = taqninmeta.TaqninData.name;
                var activity = taqninmeta.TaqninData.activity;
                var governate = taqninmeta.TaqninData.governate;
                var unit = taqninmeta.TaqninData.unit;
                var area = taqninmeta.TaqninData.area;
                var w_man = taqninmeta.TaqninData.w_man;
                var tazalom = taqninmeta.TaqninData.tazalom;
                var study_note = taqninmeta.TaqninData.study_note;
                var income_no = taqninmeta.TaqninData.income_no;
                var status = taqninmeta.TaqninData.status;
                var shapearea = taqninmeta.TaqninData.shapearea;
                var actualarea = taqninmeta.TaqninData.actualarea;
                var geographic_person_response = taqninmeta.geographic_person_response;
                var ChangesCenterDescion = taqninmeta.ChangesCenterDescion;
                var honesty = taqninmeta.honesty;
                var RaiseSurveyors = taqninmeta.RaiseSurveyors;
                var domain = taqninmeta.Domain;
                var descion223 = taqninmeta.Descion223;
                var descionqm = taqninmeta.DescionQM;
                var responseuser = taqninmeta.responseUser;
                var responsedate = taqninmeta.responsedate;
                var landspace = taqninmeta.landspace;
                var income = taqninmeta.TaqninData.income_no;
                var shapelenght = taqninmeta.TaqninData.shapelength;
                var overlap = "";
                var overlapwith = "";
                var fullfil = taqninmeta.fullfilterms;
                var fullarea = taqninmeta.Fullarea;
                var remainingspace = taqninmeta.Remainingspace;
                var convertedspace = taqninmeta.Convertedspace;
                var overlapafter = taqninmeta.Overlap_after_range;
                var ReviewerNotes = taqninmeta.ReviewerNotes;
                var RecoveryDepartmentNotes = taqninmeta.RecoveryDepartmentNotes;
                var personupload = taqninmeta.person_upload;
                var legalfullfied = taqninmeta.LegalFullfied;
                string command = "insert into TaqninDatas VALUES('" + idno + "', '" + income + "', '" + shapelenght + "', '" + shapearea + "', '" + w_man + "', '" + overlap + "', '" + overlapwith + "', '" + status + "', '" + study_note + "', '" + tazalom + "', '" + name + "','" + activity + "','" + governate + "','" + unit + "','" + area + "','" + actualarea + "','" + personupload + "','" + study_note + "','" + descion223 + "','" + descionqm + "','" + RaiseSurveyors + "','" + fullfil + "','" + fullarea + "','" + geographic_person_response + "','" + remainingspace + "','" + convertedspace + "','" + overlapafter + "','" + ChangesCenterDescion + "','" + landspace + "','" + ReviewerNotes + "','" + RecoveryDepartmentNotes + "','" + honesty + "','" + legalfullfied + "','" + responsedate + "','" + responseuser + "')";
                OleDbCommand cmd = new OleDbCommand(command, conn);
                cmd.ExecuteNonQuery();
            }
        }
        public ActionResult activateUsers()
        {
            //   insertdata();
            var userr = db.Users.SingleOrDefault(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name);
            if (User.Identity.IsAuthenticated)
            {
                var lst = db.Users.Where(x => x.isactivated == 0).ToList();
                return View(lst);
            }
            return RedirectToAction("Login", "Account");
        }


        public ActionResult Activate(string id, int choice)
        {
            var user = db.Users.SingleOrDefault(x => x.Id == id);
            if (choice == 1)
            {
                user.isactivated = 1;

            }
            else
            {
                db.Users.Remove(user);
            }

            db.SaveChanges();
            return RedirectToAction("activateUsers");
        }

        public void createshape(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var taqnin = db.TaqninMetadata.Where(x => x.id == id).ToList()[0];

            var xx = @"F:\ShapeFile";
          
            System.IO.DirectoryInfo di = new DirectoryInfo(xx);

            var file1 = di.GetFiles().FirstOrDefault();
            if (file1 == null)
            //if (!file1.Name.Contains("shape"))
            {
                DbfFieldDesc[] lFields = new DbfFieldDesc[36];
                DbfFieldDesc fld1 = new DbfFieldDesc();
                fld1.FieldName = "IDNO";
                fld1.FieldType = DbfFieldType.Character;
                fld1.FieldLength = 100;
                lFields[0] = fld1;
                DbfFieldDesc fld2 = new DbfFieldDesc();
                fld2.FieldName = "incomeno";
                fld2.FieldType = DbfFieldType.Character;
                fld2.FieldLength = 100;
                lFields[1] = fld2;
                DbfFieldDesc fld3 = new DbfFieldDesc();
                fld3.FieldName = "LandPic";
                fld3.FieldType = DbfFieldType.Character;
                fld3.FieldLength = 100;
                lFields[2] = fld3;
                DbfFieldDesc fld4 = new DbfFieldDesc();
                fld4.FieldName = "ShapLength";
                fld4.FieldType = DbfFieldType.Character;
                fld4.FieldLength = 60;
                lFields[3] = fld4;
                DbfFieldDesc fld5 = new DbfFieldDesc();
                fld5.FieldName = "ShapArea";
                fld5.FieldType = DbfFieldType.Character;
                fld5.FieldLength = 60;
                lFields[4] = fld5;

                DbfFieldDesc fld6 = new DbfFieldDesc();
                fld6.FieldName = "WMan";
                fld6.FieldType = DbfFieldType.Character;
                fld6.FieldLength = 60;
                lFields[5] = fld6;

                DbfFieldDesc fld7 = new DbfFieldDesc();
                fld7.FieldName = "overlap";
                fld7.FieldType = DbfFieldType.Character;
                fld7.FieldLength = 60;
                lFields[6] = fld7;
                DbfFieldDesc fld8 = new DbfFieldDesc();
                fld8.FieldName = "overlapwit";
                fld8.FieldType = DbfFieldType.Character;
                fld8.FieldLength = 60;
                lFields[7] = fld8;
                DbfFieldDesc fld9 = new DbfFieldDesc();

                fld9.FieldName = "Status";
                fld9.FieldType = DbfFieldType.Character;
                fld9.FieldLength = 60;
                lFields[8] = fld9;

                DbfFieldDesc fld10 = new DbfFieldDesc();
                fld10.FieldName = "tazalom";
                fld10.FieldType = DbfFieldType.Character;
                fld10.FieldLength = 60;
                lFields[9] = fld10;

                DbfFieldDesc fld11 = new DbfFieldDesc();
                fld11.FieldName = "Name";
                fld11.FieldType = DbfFieldType.Character;
                fld11.FieldLength = 60;
                lFields[10] = fld11;
                DbfFieldDesc fld12 = new DbfFieldDesc();
                fld12.FieldName = "activity";
                fld12.FieldType = DbfFieldType.Character;
                fld12.FieldLength = 60;
                lFields[11] = fld12;

                DbfFieldDesc fld13 = new DbfFieldDesc();
                fld13.FieldName = "governate";
                fld13.FieldType = DbfFieldType.Character;
                fld13.FieldLength = 60;
                lFields[12] = fld13;

                DbfFieldDesc fld14 = new DbfFieldDesc();
                fld14.FieldName = "unit";
                fld14.FieldType = DbfFieldType.Character;
                fld14.FieldLength = 60;
                lFields[13] = fld14;

                DbfFieldDesc fld15 = new DbfFieldDesc();
                fld15.FieldName = "area";
                fld15.FieldType = DbfFieldType.Character;
                fld15.FieldLength = 16;
                lFields[14] = fld15;

                DbfFieldDesc fld16 = new DbfFieldDesc();
                fld16.FieldName = "actualarea";
                fld16.FieldType = DbfFieldType.Character;
                fld16.FieldLength = 60;
                lFields[15] = fld16;

                DbfFieldDesc fld17 = new DbfFieldDesc();
                fld17.FieldName = "perUpload";
                fld17.FieldType = DbfFieldType.Character;
                fld17.FieldLength = 60;
                lFields[16] = fld17;
                DbfFieldDesc fld18 = new DbfFieldDesc();
                fld18.FieldName = "StudyUser";
                fld18.FieldType = DbfFieldType.Character;
                fld18.FieldLength = 60;
                lFields[17] = fld18;

                DbfFieldDesc fld19 = new DbfFieldDesc();
                fld19.FieldName = "Studynotes";
                fld19.FieldType = DbfFieldType.Character;
                fld19.FieldLength = 100;
                lFields[18] = fld19;

                DbfFieldDesc fd20 = new DbfFieldDesc();
                fd20.FieldName = "Descion223";
                fd20.FieldType = DbfFieldType.Character;
                fd20.FieldLength = 100;
                lFields[19] = fd20;


                DbfFieldDesc fld21 = new DbfFieldDesc();
                fld21.FieldName = "DescionQM";
                fld21.FieldType = DbfFieldType.Character;
                fld21.FieldLength = 60;
                lFields[20] = fld21;


                DbfFieldDesc fld22 = new DbfFieldDesc();
                fld22.FieldName = "RaiseSurvy";
                fld22.FieldType = DbfFieldType.Character;
                fld22.FieldLength = 60;
                lFields[21] = fld22;

                DbfFieldDesc fld23 = new DbfFieldDesc();
                fld23.FieldName = "fullfilter";
                fld23.FieldType = DbfFieldType.Character;
                fld23.FieldLength = 60;
                lFields[22] = fld23;

                DbfFieldDesc fld24 = new DbfFieldDesc();
                fld24.FieldName = "Fullarea";
                fld24.FieldType = DbfFieldType.Character;
                fld24.FieldLength = 60;
                lFields[23] = fld24;

                DbfFieldDesc fld25 = new DbfFieldDesc();
                fld25.FieldName = "geoDescion";
                fld25.FieldType = DbfFieldType.Character;
                fld25.FieldLength = 60;
                lFields[24] = fld25;


                DbfFieldDesc fld26 = new DbfFieldDesc();
                fld26.FieldName = "RmainSpace";
                fld26.FieldType = DbfFieldType.Character;
                fld26.FieldLength = 60;
                lFields[25] = fld26;

                DbfFieldDesc fld27 = new DbfFieldDesc();
                fld27.FieldName = "Conv_space";
                fld27.FieldType = DbfFieldType.Character;
                fld27.FieldLength = 60;
                lFields[26] = fld27;

                DbfFieldDesc fld28 = new DbfFieldDesc();
                fld28.FieldName = "afterRange";
                fld28.FieldType = DbfFieldType.Character;
                fld28.FieldLength = 60;
                lFields[27] = fld28;

                DbfFieldDesc fld29 = new DbfFieldDesc();
                fld29.FieldName = "ChangesDes";
                fld29.FieldType = DbfFieldType.Character;
                fld29.FieldLength = 60;
                lFields[28] = fld29;

                DbfFieldDesc fld30 = new DbfFieldDesc();
                fld30.FieldName = "landspace";
                fld30.FieldType = DbfFieldType.Character;
                fld30.FieldLength = 60;
                lFields[29] = fld30;

                DbfFieldDesc fld31 = new DbfFieldDesc();
                fld31.FieldName = "ReviwerNot";
                fld31.FieldType = DbfFieldType.Character;
                fld31.FieldLength = 100;
                lFields[30] = fld31;

                DbfFieldDesc fld32 = new DbfFieldDesc();
                fld32.FieldName = "RcovryNote";
                fld32.FieldType = DbfFieldType.Character;
                fld32.FieldLength = 60;
                lFields[31] = fld32;

                DbfFieldDesc fld33 = new DbfFieldDesc();
                fld33.FieldName = "Reviewer";
                fld33.FieldType = DbfFieldType.Character;
                fld33.FieldLength = 60;
                lFields[32] = fld33;

                DbfFieldDesc fld34 = new DbfFieldDesc();
                fld34.FieldName = "Fullfied";
                fld34.FieldType = DbfFieldType.Character;
                fld34.FieldLength = 60;
                lFields[33] = fld34;

                DbfFieldDesc fld35 = new DbfFieldDesc();
                fld35.FieldName = "RsponsDte";
                fld35.FieldType = DbfFieldType.Character;
                fld35.FieldLength = 60;
                lFields[34] = fld35;


                DbfFieldDesc fld36 = new DbfFieldDesc();
                fld36.FieldName = "RsponsUser";
                fld36.FieldType = DbfFieldType.Character;
                fld36.FieldLength = 60;
                lFields[35] = fld36;
            
                ShapeFileWriter sfw = ShapeFileWriter.CreateWriter(xx, "shape", EGIS.ShapeFileLib.ShapeType.Polygon, lFields);
                List<Coordinate> coordinateLst = new List<Coordinate>();
                coordinateLst = GetCoordinates(taqnin);
                PointD[] lPoints = new PointD[coordinateLst.Count];
                for (int counter = 0; counter < coordinateLst.Count; counter++)
                {
                    lPoints[counter] = new PointD(coordinateLst[counter].X, coordinateLst[counter].Y);

                }
                var p = (@"F:\MAI\DocumentsAndImages\") + taqnin.TaqninData.income_no + "_" + taqnin.TaqninData.id_no.ToString();

                String[] lFieldValues = new String[36];
                lFieldValues[0] = taqnin.TaqninData.id_no;
                lFieldValues[1] = taqnin.TaqninData.income_no;
                lFieldValues[2] = p;
                lFieldValues[3] = taqnin.TaqninData.shapelength.ToString();
                lFieldValues[4] = taqnin.TaqninData.shapearea.ToString();
                lFieldValues[5] = taqnin.TaqninData.w_man;
                lFieldValues[6] = "";
                lFieldValues[7] = "";
                lFieldValues[8] = taqnin.TaqninData.status;
                lFieldValues[9] = taqnin.TaqninData.tazalom;
                lFieldValues[10] = taqnin.TaqninData.name;
                lFieldValues[11] = taqnin.TaqninData.activity;
                lFieldValues[12] = taqnin.TaqninData.governate;
                lFieldValues[13] = taqnin.TaqninData.unit;
                lFieldValues[14] = taqnin.TaqninData.area.ToString();
                lFieldValues[15] = taqnin.TaqninData.actualarea.ToString();
                lFieldValues[16] = taqnin.person_upload;
                lFieldValues[17] = taqnin.studentUser == null ? "" : taqnin.studentUser;
                lFieldValues[18] = taqnin.studynotes == null ? "" : taqnin.studynotes;
                lFieldValues[19] = taqnin.Descion223 == null ? "" : taqnin.Descion223;
                lFieldValues[20] = taqnin.DescionQM == null ? "" : taqnin.DescionQM;
                lFieldValues[21] = taqnin.RaiseSurveyors == null ? "" : taqnin.RaiseSurveyors;
                lFieldValues[22] = taqnin.fullfilterms == null ? "" : taqnin.fullfilterms;
                lFieldValues[23] = taqnin.Fullarea.ToString();
                lFieldValues[24] = taqnin.geographic_person_response == null ? "" : taqnin.geographic_person_response;
                lFieldValues[25] = taqnin.Remainingspace.ToString();
                lFieldValues[26] = taqnin.Convertedspace.ToString();
                lFieldValues[27] = taqnin.Overlap_after_range == null ? "" : taqnin.Overlap_after_range;
                lFieldValues[28] = taqnin.ChangesCenterDescion == null ? "" : taqnin.ChangesCenterDescion;
                lFieldValues[29] = taqnin.landspace.ToString();
                lFieldValues[30] = taqnin.ReviewerNotes == null ? "" : taqnin.ReviewerNotes;
                lFieldValues[31] = taqnin.RecoveryDepartmentNotes == null ? "" : taqnin.RecoveryDepartmentNotes;
                lFieldValues[32] = taqnin.Reviewer == null ? "" : taqnin.Reviewer;
                lFieldValues[33] = taqnin.LegalFullfied == null ? "" : taqnin.LegalFullfied;
                lFieldValues[34] = DateTime.Now.ToString("dd/MM/yyyy");
                lFieldValues[35] = System.Web.HttpContext.Current.User.Identity.Name;
                sfw.AddRecord(lPoints, coordinateLst.Count, lFieldValues);



                sfw.Close();

            }
            else
            {
                var p = (@"F:\MAI\DocumentsAndImages\") + taqnin.TaqninData.income_no + "_" + taqnin.TaqninData.id_no.ToString();

                try
                {
                     ShapeFileWriter shap = ShapeFileWriter.OpenWriter(xx, "shape");

                    List<Coordinate> coordinateLst = new List<Coordinate>();
                    coordinateLst = GetCoordinates(taqnin);
                    PointD[] lPoints = new PointD[coordinateLst.Count];
                    for (int counter = 0; counter < coordinateLst.Count; counter++)
                    {
                        lPoints[counter] = new PointD(coordinateLst[counter].X, coordinateLst[counter].Y);
                    }

                    String[] lFieldValues = new String[36];
                    lFieldValues[0] = taqnin.TaqninData.id_no;
                    lFieldValues[1] = taqnin.TaqninData.income_no;
                    lFieldValues[2] = p;
                    lFieldValues[3] = taqnin.TaqninData.shapelength.ToString();
                    lFieldValues[4] = taqnin.TaqninData.shapearea.ToString();
                    lFieldValues[5] = taqnin.TaqninData.w_man;
                    lFieldValues[6] = "";
                    lFieldValues[7] = "";
                    lFieldValues[8] = taqnin.TaqninData.status;
                    lFieldValues[9] = taqnin.TaqninData.tazalom;
                    lFieldValues[10] = taqnin.TaqninData.name;
                    lFieldValues[11] = taqnin.TaqninData.activity;
                    lFieldValues[12] = taqnin.TaqninData.governate;
                    lFieldValues[13] = taqnin.TaqninData.unit;
                    lFieldValues[14] = taqnin.TaqninData.area.ToString();
                    lFieldValues[15] = taqnin.TaqninData.actualarea.ToString();
                    lFieldValues[16] = taqnin.person_upload;
                    lFieldValues[17] = taqnin.studentUser == null ? "" : taqnin.studentUser;
                    lFieldValues[18] = taqnin.studynotes == null ? "" : taqnin.studynotes;
                    lFieldValues[19] = taqnin.Descion223 == null ? "" : taqnin.Descion223;
                    lFieldValues[20] = taqnin.DescionQM == null ? "" : taqnin.DescionQM;
                    lFieldValues[21] = taqnin.RaiseSurveyors == null ? "" : taqnin.RaiseSurveyors;
                    lFieldValues[22] = taqnin.fullfilterms == null ? "" : taqnin.fullfilterms;
                    lFieldValues[23] = taqnin.Fullarea.ToString();
                    lFieldValues[24] = taqnin.geographic_person_response == null ? "" : taqnin.geographic_person_response;
                    lFieldValues[25] = taqnin.Remainingspace.ToString();
                    lFieldValues[26] = taqnin.Convertedspace.ToString();
                    lFieldValues[27] = taqnin.Overlap_after_range == null ? "" : taqnin.Overlap_after_range;
                    lFieldValues[28] = taqnin.ChangesCenterDescion == null ? "" : taqnin.ChangesCenterDescion;
                    lFieldValues[29] = taqnin.landspace.ToString();
                    lFieldValues[30] = taqnin.ReviewerNotes == null ? "" : taqnin.ReviewerNotes;
                    lFieldValues[31] = taqnin.RecoveryDepartmentNotes == null ? "" : taqnin.RecoveryDepartmentNotes;
                    lFieldValues[32] = taqnin.Reviewer == null ? "" : taqnin.Reviewer;
                    lFieldValues[33] = taqnin.LegalFullfied == null ? "" : taqnin.LegalFullfied;
                    lFieldValues[34] = DateTime.Now.ToString("dd/MM/yyyy");
                    lFieldValues[35] = System.Web.HttpContext.Current.User.Identity.Name;


                    //var w=lFields.length
                    shap.AddRecord(lPoints, coordinateLst.Count, lFieldValues);



                    shap.Close();
                    shap.Dispose();


                }

                catch (Exception e)
                {
                   

                }
            }




        }



        public List<Coordinate> GetCoordinates(TaqninMetadata model)
        {
            
            var polygond = model.TaqninData.PolygonData.ProviderValue.ToString();
            var CoordinateS = polygond.Substring(polygond.IndexOf("((") + 2);
            List<Coordinate> coords = new List<Coordinate>();
            int freq = CoordinateS.Where(x => (x == ',')).Count();
            double xcoordinate = 0;
            double y = 0;
            string str = "";
            string[] words = CoordinateS.Split(',');


            for (int i = 0; i < freq + 1; i++)
            {

                str = words[i].TrimStart();
                str = str.Replace(@"(", string.Empty);
                str = str.Replace(@")", string.Empty);
                string[] w = str.Split(' ');
                xcoordinate = double.Parse(w[0]);
                if (i != freq)
                    y = double.Parse(w[1]);
                else
                {
                    string last = (w[1]);
                    y = double.Parse(last.Replace("))", ""));
                }
                coords.Add(new Coordinate(xcoordinate, y));
            }

            return coords;
        }
      

        public void insertdata()
        {


            var lstd = db.TaqninMetadata.ToList();
            //var points = GetCoordinates(lstd).Count();


            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
           @"Data source= F:\New folder (6)\Mxd\CDCDATA_TEMP.mdb";
            conn.Open();

            string command = "DELETE FROM CDCDATA_A;";
            OleDbCommand cmd = new OleDbCommand(command, conn);
            cmd.ExecuteNonQuery();

            for (int x = 0; x < lstd.Count(); x++)
            {

                using (OleDbCommand insertCommand = new OleDbCommand("INSERT INTO CDCDATA_A ([ID_NO],[income_no],[LandPic],[Shape_Length],[Shape_Area],[WMan],[Status],[study_note],[tazalom],[Name],[activity],[governate],[unit],[area],[actualarea],[person_upload],[studyUser],[studynotes],[Descion223],[DescionQM],[RaiseSurveyors],[fullfilterms],[Fullarea],[geographic_descion],[Remainingspace],[Convertedspace],[Overlap_after_range],[ChangesCenterDescion],[landspace],[ReviewerNotes],[RecoveryDepartmentNotes],[honesty],[Reviewer],[LegalFullfied],[ResponseDate],[ResponseUser],[ResponseStatus])VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", conn))
                  {
                    var p = (@"F:\MAI\DocumentsAndImages\") + lstd[x].TaqninData.income_no + "_" + lstd[x].TaqninData.id_no.ToString();



                    insertCommand.Parameters.AddWithValue("@ID_NO", lstd[x].TaqninData.id_no);
                    insertCommand.Parameters.AddWithValue("@income_no", lstd[x].TaqninData.income_no);
                    insertCommand.Parameters.AddWithValue("@LandPic", p);
                    insertCommand.Parameters.AddWithValue("@Shape_Length", lstd[x].TaqninData.shapelength);
                    insertCommand.Parameters.AddWithValue("@Shape_Area", lstd[x].TaqninData.shapearea);
                    insertCommand.Parameters.AddWithValue("@WMan", lstd[x].TaqninData.w_man);
                    insertCommand.Parameters.AddWithValue("@Status", lstd[x].TaqninData.status);
                    insertCommand.Parameters.AddWithValue("@study_note", lstd[x].TaqninData.study_note);
                    insertCommand.Parameters.AddWithValue("@tazalom", lstd[x].TaqninData.tazalom);
                    insertCommand.Parameters.AddWithValue("@Name", lstd[x].TaqninData.name);
                    insertCommand.Parameters.AddWithValue("@activity", lstd[x].TaqninData.activity);
                    insertCommand.Parameters.AddWithValue("@governate", lstd[x].TaqninData.governate);
                    insertCommand.Parameters.AddWithValue("@unit", lstd[x].TaqninData.unit);
                    insertCommand.Parameters.AddWithValue("@area", lstd[x].TaqninData.area);
                    insertCommand.Parameters.AddWithValue("@actualarea", lstd[x].TaqninData.actualarea);
                    insertCommand.Parameters.AddWithValue("@person_upload", lstd[x].person_upload);
                    insertCommand.Parameters.AddWithValue("@studyUser", lstd[x].studentUser == null ? "" : lstd[x].studentUser);
                    insertCommand.Parameters.AddWithValue("@studynotes", lstd[x].studynotes == null ? "" : lstd[x].studynotes);
                    insertCommand.Parameters.AddWithValue("@Descion223", lstd[x].Descion223 == null ? "" : lstd[x].Descion223);
                    insertCommand.Parameters.AddWithValue("@DescionQM", lstd[x].DescionQM == null ? "" : lstd[x].DescionQM);
                    insertCommand.Parameters.AddWithValue("@RaiseSurveyors", lstd[x].RaiseSurveyors == null ? "" : lstd[x].RaiseSurveyors);
                    insertCommand.Parameters.AddWithValue("@fullfilterms", lstd[x].AreaFullfied == null ? "" : lstd[x].AreaFullfied);
                    insertCommand.Parameters.AddWithValue("@Fullarea", lstd[x].Fullarea);
                    insertCommand.Parameters.AddWithValue("@geographic_descion", lstd[x].geographic_person_response == null ? "" : lstd[x].geographic_person_response);
                    insertCommand.Parameters.AddWithValue("@Remainingspace", lstd[x].Remainingspace);
                    insertCommand.Parameters.AddWithValue("@Convertedspace", lstd[x].Convertedspace);
                    insertCommand.Parameters.AddWithValue("@Overlap_after_range", lstd[x].Overlap_after_range == null ? "" : lstd[x].Overlap_after_range);
                    insertCommand.Parameters.AddWithValue("@ChangesCenterDescion", lstd[x].ChangesCenterDescion == null ? "" : lstd[x].ChangesCenterDescion);
                    insertCommand.Parameters.AddWithValue("@landspace", lstd[x].landspace);
                    insertCommand.Parameters.AddWithValue("@ReviewerNotes", lstd[x].ReviewerNotes == null ? "" : lstd[x].ReviewerNotes);
                    insertCommand.Parameters.AddWithValue("@RecoveryDepartmentNotes", lstd[x].RecoveryDepartmentNotes == null ? "" : lstd[x].RecoveryDepartmentNotes);
                    insertCommand.Parameters.AddWithValue("@honesty", lstd[x].honesty == null ? "" : lstd[x].honesty);
                    insertCommand.Parameters.AddWithValue("@Reviewer", lstd[x].Reviewer == null ? "" : lstd[x].Reviewer == null ? "" : lstd[x].Reviewer);

                    insertCommand.Parameters.AddWithValue("@LegalFullfied", lstd[x].LegalFullfied == null ? "" : lstd[x].LegalFullfied == null ? "" : lstd[x].LegalFullfied);
                    insertCommand.Parameters.AddWithValue("@ResponseDate", lstd[x].responsedate == null ? "" : lstd[x].responsedate);

                    insertCommand.Parameters.AddWithValue("@ResponseUser", lstd[x].responseUser == null ? "" : lstd[x].responseUser == null ? "" : lstd[x].responseUser);
                    insertCommand.Parameters.AddWithValue("@ResponseStatus", lstd[x].responsestatus == null ? "" : lstd[x].responsestatus == null ? "" : lstd[x].responsestatus);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }

        public void updatemdbdata(int id)
        {


            var lstd = db.TaqninMetadata.Where(x => x.id == id).ToList()[0];
            var p = (@"F:\MAI\DocumentsAndImages\") + lstd.TaqninData.income_no + "_" + lstd.TaqninData.id_no.ToString();
            //var points = GetCoordinates(lstd).Count();


            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
           @"Data source= F:\New folder\Mxd\CDCDATA_TEMP.mdb";


            using (OleDbCommand insertCommand = new OleDbCommand("INSERT INTO CDCDATA_A ([ID_NO],[income_no],[LandPic],[Shape_Length],[Shape_Area],[WMan],[Status],[study_note],[tazalom],[Name],[activity],[governate],[unit],[area],[actualarea],[person_upload],[studyUser],[studynotes],[Descion223],[DescionQM],[RaiseSurveyors],[fullfilterms],[Fullarea],[geographic_descion],[Remainingspace],[Convertedspace],[Overlap_after_range],[ChangesCenterDescion],[landspace],[ReviewerNotes],[RecoveryDepartmentNotes],[honesty],[Reviewer],[LegalFullfied],[ResponseDate],[ResponseUser])VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", conn))
            {
                conn.Open();

                insertCommand.Parameters.AddWithValue("@ID_NO", lstd.TaqninData.id_no);
                insertCommand.Parameters.AddWithValue("@income_no", lstd.TaqninData.income_no);
                insertCommand.Parameters.AddWithValue("@LandPic", p);
                insertCommand.Parameters.AddWithValue("@Shape_Length", lstd.TaqninData.shapelength);
                insertCommand.Parameters.AddWithValue("@Shape_Area", lstd.TaqninData.shapearea);
                insertCommand.Parameters.AddWithValue("@WMan", lstd.TaqninData.w_man);
                insertCommand.Parameters.AddWithValue("@Status", lstd.TaqninData.status);
                insertCommand.Parameters.AddWithValue("@study_note", lstd.TaqninData.study_note);
                insertCommand.Parameters.AddWithValue("@tazalom", lstd.TaqninData.tazalom);
                insertCommand.Parameters.AddWithValue("@Name", lstd.TaqninData.name);
                insertCommand.Parameters.AddWithValue("@activity", lstd.TaqninData.activity);
                insertCommand.Parameters.AddWithValue("@governate", lstd.TaqninData.governate);
                insertCommand.Parameters.AddWithValue("@unit", lstd.TaqninData.unit);
                insertCommand.Parameters.AddWithValue("@area", lstd.TaqninData.area);
                insertCommand.Parameters.AddWithValue("@actualarea", lstd.TaqninData.actualarea);
                insertCommand.Parameters.AddWithValue("@person_upload", lstd.person_upload);
                insertCommand.Parameters.AddWithValue("@studyUser", lstd.studentUser);
                insertCommand.Parameters.AddWithValue("@studynotes", lstd.studynotes);
                insertCommand.Parameters.AddWithValue("@Descion223", lstd.Descion223 == null ? "" : lstd.Descion223);
                insertCommand.Parameters.AddWithValue("@DescionQM", lstd.DescionQM == null ? "" : lstd.DescionQM);
                insertCommand.Parameters.AddWithValue("@RaiseSurveyors", lstd.RaiseSurveyors == null ? "" : lstd.RaiseSurveyors);
                insertCommand.Parameters.AddWithValue("@fullfilterms", lstd.AreaFullfied == null ? "" : lstd.AreaFullfied);
                insertCommand.Parameters.AddWithValue("@Fullarea", lstd.Fullarea);
                insertCommand.Parameters.AddWithValue("@geographic_descion", lstd.geographic_person_response == null ? "" : lstd.geographic_person_response);
                insertCommand.Parameters.AddWithValue("@Remainingspace", lstd.Remainingspace);
                insertCommand.Parameters.AddWithValue("@Convertedspace", lstd.Convertedspace);
                insertCommand.Parameters.AddWithValue("@Overlap_after_range", lstd.Overlap_after_range == null ? "" : lstd.Overlap_after_range);
                insertCommand.Parameters.AddWithValue("@ChangesCenterDescion", lstd.ChangesCenterDescion == null ? "" : lstd.ChangesCenterDescion);
                insertCommand.Parameters.AddWithValue("@landspace", lstd.landspace);
                insertCommand.Parameters.AddWithValue("@ReviewerNotes", lstd.ReviewerNotes == null ? "" : lstd.ReviewerNotes);
                insertCommand.Parameters.AddWithValue("@RecoveryDepartmentNotes", lstd.RecoveryDepartmentNotes == null ? "" : lstd.RecoveryDepartmentNotes);
                insertCommand.Parameters.AddWithValue("@honesty", lstd.honesty == null ? "" : lstd.honesty);
                insertCommand.Parameters.AddWithValue("@Reviewer", lstd.Reviewer == null ? "" : lstd.Reviewer);
                //insertCommand.Parameters.AddWithValue("@point", points);
                insertCommand.Parameters.AddWithValue("@LegalFullfied", lstd.LegalFullfied == null ? "" : lstd.LegalFullfied);
                insertCommand.Parameters.AddWithValue("@ResponseDate", lstd.responsedate == null ? "" : lstd.responsedate);

                insertCommand.Parameters.AddWithValue("@ResponseUser", lstd.responseUser == null ? "" : lstd.responseUser);
                insertCommand.ExecuteNonQuery();
            }
        }

        //backup of database
        public void updatemdbdataall()
        {



            var lstd = db.TaqninMetadata.Where(x=>x.OrderStatus == "تم الرد والحفظ فى الارشيف").ToList();

          


            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
           @"Data source= F:\ArchiveData\New folder\CDCDATA_TEMP.mdb";
            conn.Open();

            string command = "DELETE FROM CDCDATA_A;";
            OleDbCommand cmd = new OleDbCommand(command, conn);
            cmd.ExecuteNonQuery();

            for (int x = 0; x < lstd.Count; x++)
            {


                using (OleDbCommand insertCommand = new OleDbCommand("INSERT INTO CDCDATA_A ([ID_NO],[income_no],[LandPic],[Shape_Length],[Shape_Area],[WMan],[Status],[study_note],[tazalom],[Name],[activity],[governate],[unit],[area],[actualarea],[person_upload],[studyUser],[studynotes],[Descion223],[DescionQM],[RaiseSurveyors],[fullfilterms],[Fullarea],[geographic_descion],[Remainingspace],[Convertedspace],[Overlap_after_range],[ChangesCenterDescion],[landspace],[ReviewerNotes],[RecoveryDepartmentNotes],[honesty],[Reviewer],[LegalFullfied],[ResponseDate],[ResponseUser])VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", conn))
                {
                    var p = (@"F:\MAI\DocumentsAndImages\") + lstd[x].TaqninData.income_no + "_" + lstd[x].TaqninData.id_no.ToString();
                    insertCommand.Parameters.AddWithValue("@ID_NO", lstd[x].TaqninData.id_no);
                    insertCommand.Parameters.AddWithValue("@income_no", lstd[x].TaqninData.income_no);
                    insertCommand.Parameters.AddWithValue("@LandPic", p);
                    insertCommand.Parameters.AddWithValue("@Shape_Length", lstd[x].TaqninData.shapelength);
                    insertCommand.Parameters.AddWithValue("@Shape_Area", lstd[x].TaqninData.shapearea);
                    insertCommand.Parameters.AddWithValue("@WMan", lstd[x].TaqninData.w_man == null ? "" : lstd[x].TaqninData.w_man);
                    insertCommand.Parameters.AddWithValue("@Status", lstd[x].TaqninData.status == null ? "" : lstd[x].TaqninData.status);
                    insertCommand.Parameters.AddWithValue("@study_note", lstd[x].TaqninData.study_note == null ? "" : lstd[x].TaqninData.study_note);
                    insertCommand.Parameters.AddWithValue("@tazalom", lstd[x].TaqninData.tazalom == null ? "" : lstd[x].TaqninData.tazalom);
                    insertCommand.Parameters.AddWithValue("@Name", lstd[x].TaqninData.name == null ? "" : lstd[x].TaqninData.name);
                    insertCommand.Parameters.AddWithValue("@activity", lstd[x].TaqninData.activity == null ? "" : lstd[x].TaqninData.activity);
                    insertCommand.Parameters.AddWithValue("@governate", lstd[x].TaqninData.governate == null ? "" : lstd[x].TaqninData.governate);
                    insertCommand.Parameters.AddWithValue("@unit", lstd[x].TaqninData.unit);
                    insertCommand.Parameters.AddWithValue("@area", lstd[x].TaqninData.area);
                    insertCommand.Parameters.AddWithValue("@actualarea", lstd[x].TaqninData.actualarea);
                    insertCommand.Parameters.AddWithValue("@person_upload", lstd[x].person_upload == null ? "" : lstd[x].person_upload);
                    insertCommand.Parameters.AddWithValue("@studyUser", lstd[x].studentUser == null ? "" : lstd[x].studentUser);
                    insertCommand.Parameters.AddWithValue("@studynotes", lstd[x].studynotes == null ? "" : lstd[x].studynotes);
                    insertCommand.Parameters.AddWithValue("@Descion223", lstd[x].Descion223 == null ? "" : lstd[x].Descion223);
                    insertCommand.Parameters.AddWithValue("@DescionQM", lstd[x].DescionQM == null ? "" : lstd[x].DescionQM);
                    insertCommand.Parameters.AddWithValue("@RaiseSurveyors", lstd[x].RaiseSurveyors == null ? "" : lstd[x].RaiseSurveyors);
                    insertCommand.Parameters.AddWithValue("@fullfilterms", lstd[x].AreaFullfied == null ? "" : lstd[x].AreaFullfied);
                    insertCommand.Parameters.AddWithValue("@Fullarea", lstd[x].Fullarea);
                    insertCommand.Parameters.AddWithValue("@geographic_descion", lstd[x].geographic_person_response == null ? "" : lstd[x].geographic_person_response);
                    insertCommand.Parameters.AddWithValue("@Remainingspace", lstd[x].Remainingspace);
                    insertCommand.Parameters.AddWithValue("@Convertedspace", lstd[x].Convertedspace);
                    insertCommand.Parameters.AddWithValue("@Overlap_after_range", lstd[x].Overlap_after_range == null ? "" : lstd[x].Overlap_after_range);
                    insertCommand.Parameters.AddWithValue("@ChangesCenterDescion", lstd[x].ChangesCenterDescion == null ? "" : lstd[x].ChangesCenterDescion);
                    insertCommand.Parameters.AddWithValue("@landspace", lstd[x].landspace);
                    insertCommand.Parameters.AddWithValue("@ReviewerNotes", lstd[x].ReviewerNotes == null ? "" : lstd[x].ReviewerNotes);
                    insertCommand.Parameters.AddWithValue("@RecoveryDepartmentNotes", lstd[x].RecoveryDepartmentNotes == null ? "" : lstd[x].RecoveryDepartmentNotes);
                    insertCommand.Parameters.AddWithValue("@honesty", lstd[x].honesty == null ? "" : lstd[x].honesty);
                    insertCommand.Parameters.AddWithValue("@Reviewer", lstd[x].Reviewer == null ? "" : lstd[x].Reviewer);
                     insertCommand.Parameters.AddWithValue("@LegalFullfied", lstd[x].LegalFullfied == null ? "" : lstd[x].LegalFullfied);
                    insertCommand.Parameters.AddWithValue("@ResponseDate", lstd[x].responsedate == null ? "" : lstd[x].responsedate);

                    insertCommand.Parameters.AddWithValue("@ResponseUser", lstd[x].responseUser == null ? "" : lstd[x].responseUser);
                    insertCommand.ExecuteNonQuery();
                }

            }
        }

        public ActionResult backupdbdata()
        {



            var lstd = db.TaqninMetadata.ToList();




            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
           @"Data source= F:\DatabaseBackup\CDCDATA_TEMP.mdb";
            conn.Open();

            string command = "DELETE FROM CDCDATA_A;";
            OleDbCommand cmd = new OleDbCommand(command, conn);
            cmd.ExecuteNonQuery();

            for (int x = 0; x < lstd.Count; x++)
            {


                using (OleDbCommand insertCommand = new OleDbCommand("INSERT INTO CDCDATA_A ([ID_NO],[income_no],[LandPic],[Shape_Length],[Shape_Area],[WMan],[Status],[study_note],[tazalom],[Name],[activity],[governate],[unit],[area],[actualarea],[person_upload],[studyUser],[studynotes],[Descion223],[DescionQM],[RaiseSurveyors],[fullfilterms],[Fullarea],[geographic_descion],[Remainingspace],[Convertedspace],[Overlap_after_range],[ChangesCenterDescion],[landspace],[ReviewerNotes],[RecoveryDepartmentNotes],[honesty],[Reviewer],[LegalFullfied],[ResponseDate],[ResponseUser],[ResponseStatus])VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", conn))
                {
                    var p = (@"F:\MAI\DocumentsAndImages\") + lstd[x].TaqninData.income_no + "_" + lstd[x].TaqninData.id_no.ToString();
                    insertCommand.Parameters.AddWithValue("@ID_NO", lstd[x].TaqninData.id_no);
                    insertCommand.Parameters.AddWithValue("@income_no", lstd[x].TaqninData.income_no);
                    insertCommand.Parameters.AddWithValue("@LandPic", p);
                    insertCommand.Parameters.AddWithValue("@Shape_Length", lstd[x].TaqninData.shapelength);
                    insertCommand.Parameters.AddWithValue("@Shape_Area", lstd[x].TaqninData.shapearea);
                    insertCommand.Parameters.AddWithValue("@WMan", lstd[x].TaqninData.w_man == null ? "" : lstd[x].TaqninData.w_man);
                    insertCommand.Parameters.AddWithValue("@Status", lstd[x].TaqninData.status == null ? "" : lstd[x].TaqninData.status);
                    insertCommand.Parameters.AddWithValue("@study_note", lstd[x].TaqninData.study_note == null ? "" : lstd[x].TaqninData.study_note);
                    insertCommand.Parameters.AddWithValue("@tazalom", lstd[x].TaqninData.tazalom == null ? "" : lstd[x].TaqninData.tazalom);
                    insertCommand.Parameters.AddWithValue("@Name", lstd[x].TaqninData.name == null ? "" : lstd[x].TaqninData.name);
                    insertCommand.Parameters.AddWithValue("@activity", lstd[x].TaqninData.activity == null ? "" : lstd[x].TaqninData.activity);
                    insertCommand.Parameters.AddWithValue("@governate", lstd[x].TaqninData.governate == null ? "" : lstd[x].TaqninData.governate);
                    insertCommand.Parameters.AddWithValue("@unit", lstd[x].TaqninData.unit);
                    insertCommand.Parameters.AddWithValue("@area", lstd[x].TaqninData.area);
                    insertCommand.Parameters.AddWithValue("@actualarea", lstd[x].TaqninData.actualarea);
                    insertCommand.Parameters.AddWithValue("@person_upload", lstd[x].person_upload == null ? "" : lstd[x].person_upload);
                    insertCommand.Parameters.AddWithValue("@studyUser", lstd[x].studentUser == null ? "" : lstd[x].studentUser);
                    insertCommand.Parameters.AddWithValue("@studynotes", lstd[x].studynotes == null ? "" : lstd[x].studynotes);
                    insertCommand.Parameters.AddWithValue("@Descion223", lstd[x].Descion223 == null ? "" : lstd[x].Descion223);
                    insertCommand.Parameters.AddWithValue("@DescionQM", lstd[x].DescionQM == null ? "" : lstd[x].DescionQM);
                    insertCommand.Parameters.AddWithValue("@RaiseSurveyors", lstd[x].RaiseSurveyors == null ? "" : lstd[x].RaiseSurveyors);
                    insertCommand.Parameters.AddWithValue("@fullfilterms", lstd[x].AreaFullfied == null ? "" : lstd[x].AreaFullfied);
                    insertCommand.Parameters.AddWithValue("@Fullarea", lstd[x].Fullarea);
                    insertCommand.Parameters.AddWithValue("@geographic_descion", lstd[x].geographic_person_response == null ? "" : lstd[x].geographic_person_response);
                    insertCommand.Parameters.AddWithValue("@Remainingspace", lstd[x].Remainingspace);
                    insertCommand.Parameters.AddWithValue("@Convertedspace", lstd[x].Convertedspace);
                    insertCommand.Parameters.AddWithValue("@Overlap_after_range", lstd[x].Overlap_after_range == null ? "" : lstd[x].Overlap_after_range);
                    insertCommand.Parameters.AddWithValue("@ChangesCenterDescion", lstd[x].ChangesCenterDescion == null ? "" : lstd[x].ChangesCenterDescion);
                    insertCommand.Parameters.AddWithValue("@landspace", lstd[x].landspace);
                    insertCommand.Parameters.AddWithValue("@ReviewerNotes", lstd[x].ReviewerNotes == null ? "" : lstd[x].ReviewerNotes);
                    insertCommand.Parameters.AddWithValue("@RecoveryDepartmentNotes", lstd[x].RecoveryDepartmentNotes == null ? "" : lstd[x].RecoveryDepartmentNotes);
                    insertCommand.Parameters.AddWithValue("@honesty", lstd[x].honesty == null ? "" : lstd[x].honesty);
                    insertCommand.Parameters.AddWithValue("@Reviewer", lstd[x].Reviewer == null ? "" : lstd[x].Reviewer);
                    insertCommand.Parameters.AddWithValue("@LegalFullfied", lstd[x].LegalFullfied == null ? "" : lstd[x].LegalFullfied);
                    insertCommand.Parameters.AddWithValue("@ResponseDate", lstd[x].responsedate == null ? "" : lstd[x].responsedate);

                    insertCommand.Parameters.AddWithValue("@ResponseUser", lstd[x].responseUser == null ? "" : lstd[x].responseUser);
                    insertCommand.Parameters.AddWithValue("@ResponseStatus", lstd[x].responsestatus == null ? "" : lstd[x].responsestatus);

                    insertCommand.ExecuteNonQuery();
                }

            }
            return Json(new { success = true, message = "Backup Done" });
        }



        public ActionResult updatemdbdataallajax()
        {



            var lstd = db.TaqninMetadata.Where(x => x.OrderStatus == "تم الرد والحفظ فى الارشيف").ToList();




            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
           @"Data source= F:\Data\New folder\CDCDATA_TEMP.mdb";
            conn.Open();

            string command = "DELETE FROM CDCDATA_A;";
            OleDbCommand cmd = new OleDbCommand(command, conn);
            cmd.ExecuteNonQuery();

            for (int x = 0; x < lstd.Count; x++)
            {


                using (OleDbCommand insertCommand = new OleDbCommand("INSERT INTO CDCDATA_A ([ID_NO],[income_no],[LandPic],[Shape_Length],[Shape_Area],[WMan],[Status],[study_note],[tazalom],[Name],[activity],[governate],[unit],[area],[actualarea],[person_upload],[studyUser],[studynotes],[Descion223],[DescionQM],[RaiseSurveyors],[fullfilterms],[Fullarea],[geographic_descion],[Remainingspace],[Convertedspace],[Overlap_after_range],[ChangesCenterDescion],[landspace],[ReviewerNotes],[RecoveryDepartmentNotes],[honesty],[Reviewer],[LegalFullfied],[ResponseDate],[ResponseUser])VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", conn))
                {
                    var p = (@"F:\MAI\DocumentsAndImages\") + lstd[x].TaqninData.income_no + "_" + lstd[x].TaqninData.id_no.ToString();
                    insertCommand.Parameters.AddWithValue("@ID_NO", lstd[x].TaqninData.id_no);
                    insertCommand.Parameters.AddWithValue("@income_no", lstd[x].TaqninData.income_no);
                    insertCommand.Parameters.AddWithValue("@LandPic", p);
                    insertCommand.Parameters.AddWithValue("@Shape_Length", lstd[x].TaqninData.shapelength);
                    insertCommand.Parameters.AddWithValue("@Shape_Area", lstd[x].TaqninData.shapearea);
                    insertCommand.Parameters.AddWithValue("@WMan", lstd[x].TaqninData.w_man == null ? "" : lstd[x].TaqninData.w_man);
                    insertCommand.Parameters.AddWithValue("@Status", lstd[x].TaqninData.status == null ? "" : lstd[x].TaqninData.status);
                    insertCommand.Parameters.AddWithValue("@study_note", lstd[x].TaqninData.study_note == null ? "" : lstd[x].TaqninData.study_note);
                    insertCommand.Parameters.AddWithValue("@tazalom", lstd[x].TaqninData.tazalom == null ? "" : lstd[x].TaqninData.tazalom);
                    insertCommand.Parameters.AddWithValue("@Name", lstd[x].TaqninData.name == null ? "" : lstd[x].TaqninData.name);
                    insertCommand.Parameters.AddWithValue("@activity", lstd[x].TaqninData.activity == null ? "" : lstd[x].TaqninData.activity);
                    insertCommand.Parameters.AddWithValue("@governate", lstd[x].TaqninData.governate == null ? "" : lstd[x].TaqninData.governate);
                    insertCommand.Parameters.AddWithValue("@unit", lstd[x].TaqninData.unit);
                    insertCommand.Parameters.AddWithValue("@area", lstd[x].TaqninData.area);
                    insertCommand.Parameters.AddWithValue("@actualarea", lstd[x].TaqninData.actualarea);
                    insertCommand.Parameters.AddWithValue("@person_upload", lstd[x].person_upload == null ? "" : lstd[x].person_upload);
                    insertCommand.Parameters.AddWithValue("@studyUser", lstd[x].studentUser == null ? "" : lstd[x].studentUser);
                    insertCommand.Parameters.AddWithValue("@studynotes", lstd[x].studynotes == null ? "" : lstd[x].studynotes);
                    insertCommand.Parameters.AddWithValue("@Descion223", lstd[x].Descion223 == null ? "" : lstd[x].Descion223);
                    insertCommand.Parameters.AddWithValue("@DescionQM", lstd[x].DescionQM == null ? "" : lstd[x].DescionQM);
                    insertCommand.Parameters.AddWithValue("@RaiseSurveyors", lstd[x].RaiseSurveyors == null ? "" : lstd[x].RaiseSurveyors);
                    insertCommand.Parameters.AddWithValue("@fullfilterms", lstd[x].AreaFullfied == null ? "" : lstd[x].AreaFullfied);
                    insertCommand.Parameters.AddWithValue("@Fullarea", lstd[x].Fullarea);
                    insertCommand.Parameters.AddWithValue("@geographic_descion", lstd[x].geographic_person_response == null ? "" : lstd[x].geographic_person_response);
                    insertCommand.Parameters.AddWithValue("@Remainingspace", lstd[x].Remainingspace);
                    insertCommand.Parameters.AddWithValue("@Convertedspace", lstd[x].Convertedspace);
                    insertCommand.Parameters.AddWithValue("@Overlap_after_range", lstd[x].Overlap_after_range == null ? "" : lstd[x].Overlap_after_range);
                    insertCommand.Parameters.AddWithValue("@ChangesCenterDescion", lstd[x].ChangesCenterDescion == null ? "" : lstd[x].ChangesCenterDescion);
                    insertCommand.Parameters.AddWithValue("@landspace", lstd[x].landspace);
                    insertCommand.Parameters.AddWithValue("@ReviewerNotes", lstd[x].ReviewerNotes == null ? "" : lstd[x].ReviewerNotes);
                    insertCommand.Parameters.AddWithValue("@RecoveryDepartmentNotes", lstd[x].RecoveryDepartmentNotes == null ? "" : lstd[x].RecoveryDepartmentNotes);
                    insertCommand.Parameters.AddWithValue("@honesty", lstd[x].honesty == null ? "" : lstd[x].honesty);
                    insertCommand.Parameters.AddWithValue("@Reviewer", lstd[x].Reviewer == null ? "" : lstd[x].Reviewer);
                    insertCommand.Parameters.AddWithValue("@LegalFullfied", lstd[x].LegalFullfied == null ? "" : lstd[x].LegalFullfied);
                    insertCommand.Parameters.AddWithValue("@ResponseDate", lstd[x].responsedate == null ? "" : lstd[x].responsedate);

                    insertCommand.Parameters.AddWithValue("@ResponseUser", lstd[x].responseUser == null ? "" : lstd[x].responseUser);
                    insertCommand.ExecuteNonQuery();
                }

            }
            return Json(new { success = true, message = "Backup Done" });
        }



        public ActionResult revieweraccount()
        {

            return View();
        }


        public ActionResult DeleteOrders_account()
        {
            if (User.Identity.IsAuthenticated)
                return View();
            else return RedirectToAction("Login", "Account");
        }
        public ActionResult ShowStudyImages()
        {
            return View();
        }
        public ActionResult study_response()
        {
            return View();
        }

        public ActionResult study_ResponseDescion(string id_no, string income_no, string geographic_person_response, string governate, string studentUser, string Descion223, string name, string DescionQM, string responsedate, string tazalom,
           string RaiseSurveyors, string fullfilterms, string status, string Delayed, string activity, int? searchh, string revieweruser, string LegalFullfied, string ChangesCenterDescion)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.actioname = "study_ResponseDescion";

                ViewBag.users = new SelectList(GetUsers().ToList());
                ViewBag.governamentlst = new SelectList(GetGovernament().ToList());
                ViewBag.incomelst = new SelectList(GetIncome().ToList());
                ViewBag.Status1 = new SelectList(Status().ToList());
                ViewBag.activity1 = new SelectList(activitylst().ToList());
                ViewBag.studyUser1 = new SelectList(studyUser().ToList());
                ViewBag.Revieweruser1 = new SelectList(Revieweruser().ToList());
                ViewBag.LegalFullfied1 = new SelectList(LegalFullfiedlst().ToList());

                ViewBag.idnoselected = id_no;
                ViewBag.income_noselected = income_no;
                ViewBag.geographic_person_responseselected = geographic_person_response;
                ViewBag.governateselected = governate;
                ViewBag.studentUserselected = studentUser;
                ViewBag.Descion223selected = Descion223;
                ViewBag.nameselected = name;
                ViewBag.DescionQMselected = DescionQM;
                ViewBag.responsedateselected = responsedate;
                ViewBag.tazalomselected = tazalom;
                ViewBag.RaiseSurveyorsselected = RaiseSurveyors;
                ViewBag.fullfiltermsselected = fullfilterms;
                ViewBag.statusselected = status;
                ViewBag.Delayedselected = Delayed;
                ViewBag.activityselected = activity;
                ViewBag.revieweruserselected = revieweruser;
                ViewBag.LegalFullfiedselected = LegalFullfied;
                ViewBag.ChangesCenterDescionselected = ChangesCenterDescion;

                var lst = new List<TaqninMetadata>();
                if (searchh == 1)
                {
                    lst = searchby(id_no, income_no, geographic_person_response, governate, studentUser, Descion223, name, DescionQM, responsedate, tazalom,
                    RaiseSurveyors, fullfilterms, status, Delayed, activity, revieweruser, LegalFullfied, ChangesCenterDescion);
                    if (lst.Count != 0)
                        lst = lst.Where(x => ((x.MajorApproval == "موافق") || (x.PoineerApproval == "الرد")) && String.IsNullOrEmpty(x.responseUser) || (x.BackToresponse == 1) && x.BackToReviewer == 0 && x.BackToVariableCenter == 0 && x.BackToCaptian == 0 && x.BackToPoineer == 0).ToList();
                }

                else

                    lst = db.TaqninMetadata.Where(x => ((x.MajorApproval == "موافق") || (x.PoineerApproval == "الرد")) && String.IsNullOrEmpty(x.responseUser) || (x.BackToresponse == 1) && x.BackToReviewer == 0 && x.BackToVariableCenter == 0 && x.BackToCaptian == 0 && x.BackToPoineer == 0).ToList();
                ViewBag.count = lst.Count();
                return View(lst);
            } return RedirectToAction("Login", "Account");
        }

        public ActionResult study_ResponseData(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var taqninmeta = db.TaqninMetadata.FirstOrDefault(x => x.id == id);
                var taqninmeta_gpr = taqninmeta.geographic_person_response;

                List<string> tt = new List<string>
            {
                "خارج", "داخل"
            };

                ViewBag.Geo_person_response = new SelectList(tt, taqninmeta_gpr);

                return View(taqninmeta);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult savestudy_ResponseDescion(TaqninMetadata taqninmeta, string ResponseNotes, FormCollection frm)
        {

            var taqninmetadatasaved = db.TaqninMetadata.FirstOrDefault(x => x.id == taqninmeta.id);
            var log = new LogTable();
            log.userName = System.Web.HttpContext.Current.User.Identity.Name;
            log.id_no = taqninmetadatasaved.TaqninData.id_no;
            log.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            log.action = "الرد";
            var Descion = frm["descion"];

            switch (Descion)
            {
                case "1":
                    if (taqninmetadatasaved.ResponseApproval != "موافق")
                    {
                        taqninmetadatasaved.responsedate = DateTime.Now.ToString("dd/MM/yyyy");
                        taqninmetadatasaved.responseUser = System.Web.HttpContext.Current.User.Identity.Name;
                        taqninmetadatasaved.ResponseNotes = ResponseNotes.Trim();
                        taqninmetadatasaved.ResponseApproval = "موافق";
                        taqninmetadatasaved.responsestatus = "response";
                        taqninmetadatasaved.OrderStatus = "تم الرد والحفظ فى الارشيف";
                        taqninmetadatasaved.BackToresponse = 0;

                        taqninmetadatasaved.AreaFullfied = taqninmeta.AreaFullfied;
                        taqninmetadatasaved.LegalFullfied = taqninmeta.LegalFullfied;
                        
                        string date2 = taqninmetadatasaved.uploaddate;
                        DateTime dt = DateTime.ParseExact("14/11/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime dt1 = DateTime.ParseExact(date2, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                        if (dt1.Date > dt.Date)
                            createshape(taqninmetadatasaved.id);
                        else
                            updatemdbdata(taqninmetadatasaved.id);

                    }
                    else
                    {
                        taqninmetadatasaved.ResponseApproval = "موافق";
                        taqninmetadatasaved.responsestatus = "response";
                        taqninmetadatasaved.OrderStatus = "تم الرد والحفظ فى الارشيف";


                    }
                    break;

              

            }
            db.LogTable.Add(log);
            taqninmetadatasaved.TaqninData.Updated = System.Web.HttpContext.Current.User.Identity.Name;
            taqninmetadatasaved.TaqninData.UpdatedDevice = DetermineCompName();
            taqninmetadatasaved.TaqninData.UpdatedTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");

            db.SaveChanges();
            return RedirectToAction("study_ResponseDescion");
        }

        public ActionResult options()
        {

            var user = db.Users.SingleOrDefault(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name);
            if (User.Identity.IsAuthenticated && user.userrole == "حذف الطلبات")
                return View();
            else return RedirectToAction("Login", "Account");
        }
        public void captianfunc()
        {
                      var lst = db.TaqninMetadata.Where(x => ((((((x.ChangesCenterDescion == "مستوفي" && x.LegalFullfied == "قبل القانون") || (x.ChangesCenterDescion == "غيرمدقق")) || (x.ReviewerApproval == "موافق")) && String.IsNullOrEmpty(x.CaptianApproval) && String.IsNullOrEmpty(x.captianUser)) && x.SuspendedOrder == false) || (x.BackToCaptian == 1)) && x.responsestatus != " response").ToList();
            lst.ForEach(y =>
                {


                    y.CaptianApproval = "موافق";
                    y.captianUser = "it3";
                    y.BackToCaptian = 0;
                    y.BackToPoineer = 1;
                    y.OrderStatus = "اعتماد رئيس قسم استرداد اراضي الدوله";
                });
            db.SaveChanges();
            func1();
            func4();

        }
        [HttpPost]
        public ActionResult captianfunct()
        {
            var lst = db.TaqninMetadata.Where(x => ((((((x.ChangesCenterDescion == "مستوفي" && x.LegalFullfied == "قبل القانون") || (x.ChangesCenterDescion == "غيرمدقق")) || (x.ReviewerApproval == "موافق")) && String.IsNullOrEmpty(x.CaptianApproval) && String.IsNullOrEmpty(x.captianUser)) && x.SuspendedOrder == false) || (x.BackToCaptian == 1)) && x.responsestatus != " response").ToList();
            lst.ForEach(y =>
            {


                y.CaptianApproval = "موافق";
                y.captianUser = "it3";
                y.BackToCaptian = 0;
                y.BackToPoineer = 1;
                y.OrderStatus = "اعتماد رئيس قسم استرداد اراضي الدوله";
            });
            db.SaveChanges();
            func1();
            func4();
            return Json(new { success = true, message = "تم ارسال"+lst.Count+"من الطلبات  " });
        }
        
        [HttpPost]
        public ActionResult sendpoineerorders()
        {
            var lst = db.TaqninMetadata.Where((x => ((x.CaptianApproval == "موافق" && String.IsNullOrEmpty(x.PoineerApproval)) || x.BackToPoineer == 1) && x.SuspendedOrder == false)).ToList();
            lst.ForEach(y =>
            {


                y.PoineerApproval = "موافق";
                y.PoineerUser = "it3";
                y.BackToPoineer = 0;
                y.BackToMajor = 1;
                y.OrderStatus = "اعتماد قائد مركز المتغيرات المكانيه";
            });
            db.SaveChanges();

            func4();
            return Json(new { success = true, message = "تم ارسال" + lst.Count + "من الطلبات  " });

        
        }
         [HttpPost]
        public ActionResult exportdatabaase()
        {
            updatemdbdataall();
            return Json(new { success = true, message = "Data Exported!" });

        
        }
        
        public void func1()
        {

            var lst = db.TaqninMetadata.Where((x => ((x.CaptianApproval == "موافق" && String.IsNullOrEmpty(x.PoineerApproval)) || x.BackToPoineer == 1) && x.SuspendedOrder == false)).ToList();
            lst.ForEach(y =>
                {


                    y.PoineerApproval = "موافق";
                    y.PoineerUser = "it3";
                    y.BackToPoineer = 0;
                    y.BackToMajor = 1;
                    y.OrderStatus = "اعتماد قائد مركز المتغيرات المكانيه";
                });
            db.SaveChanges();
        }
        public void func()
        {
            var lst = db.TaqninMetadata.Where((x => ((x.PoineerApproval == "موافق" && String.IsNullOrEmpty(x.MajorApproval)) || x.BackToMajor == 1))).ToList();
            lst.ForEach(y =>
                {

                    y.OrderStatus = "تأكيد قرار";
                    y.MajorApproval = "موافق";
                    y.majorUser = "Major";
                    y.BackToMajor = 0;
                    y.BackToReviewercheck = 1;
                });
            db.SaveChanges();
        }

        public void func4()
        {

            var lst = db.TaqninMetadata.Where(x => (x.PoineerApproval == "موافق" && String.IsNullOrEmpty(x.MajorApproval)) || (x.BackToMajor == 1)).ToList();
            lst.ForEach(y =>
            {
                y.MajorApproval = "موافق";
                y.majorUser = "Major";
                y.OrderStatus = "الرد";
                y.BackToresponse = 1;
                y.BackToMajor = 0;
            });
            db.SaveChanges();
        }
      
        public static string DetermineCompName()
        { 
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            try
            {

                IPAddress myIP = IPAddress.Parse(ip);
                IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
                List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
                return compName.First();
            }
            catch (Exception e)
            {
                return ip;
            }
        }

    }

}