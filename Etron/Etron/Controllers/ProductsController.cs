using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Etron.Models;
using PagedList;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Net;
namespace Etron.Controllers
{
    public class ProductsController : Controller
    {
        private etronEntities db = new etronEntities();
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        // GET: Cats
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult List(int? pg, string search)
        {
            int pageSize = 25;
            if (pg == null) pg = 1;
            int pageNumber = (pg ?? 1);
            ViewBag.pg = pg;
            var data = db.products.Select(x => x);
            if (data == null)
            {
                return View(data);
            }
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                data = data.Where(x => x.product_name.ToLower().Contains(search));
                ViewBag.search = search;
            }

            data = data.OrderBy(x => x.updated_date);
            return View(data.ToList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ShopVn(int? pg, string search)
        {
            
            var p = (from q in db.products where q.cat_id == 14 && q.lang.Contains("vn") select q).OrderByDescending(x => x.product_id).Take(100).ToList();
            ViewBag.control = p;
            p = (from q in db.products where q.cat_id == 15 && q.lang.Contains("vn") select q).OrderByDescending(x => x.product_id).Take(100).ToList();
            ViewBag.acc = p;
            return View();
        }
        public ActionResult Detail(long id)
        {
            product p = db.products.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(ProductVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Errored"] = "Vui lòng kiểm tra lại các trường.";
                return RedirectToRoute("AdminAddProduct");
            }

            try
            {
                long? idproduct = 0;
                product _new = new product();
                _new.cat_id = model.cat_id ?? null;
                _new.product_name = model.product_name ?? null;
                _new.product_content = model.product_content ?? null;
                _new.product_photo = model.product_photo ?? null;
                _new.product_photo2 = model.product_photo2 ?? null;
                _new.product_price_public = model.product_price_public ?? null;
                //_new.product_type = model.product_type ?? null;
                _new.product_new_type = model.product_new_type ?? null;
                _new.status = model.status;
                _new.updated_date = DateTime.Now;
                _new.product_des = model.product_des ?? null;
                _new.lang = model.lang ?? null;
                _new.product_feature = model.product_feature ?? null;
                _new.product_technical = model.product_technical ?? null;
                db.products.Add(_new);
                db.SaveChanges();
                //await db.SaveChangesAsync();
                idproduct = _new.product_id;

                TempData["Updated"] = "Thêm sản phẩm thành công";
                return RedirectToRoute("AdminEditProduct", new { id = idproduct });
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi thêm mới");
                //configs.SaveTolog(ex.ToString());
                return View(model);
            }

        }

        //updateanh1
        [HttpPost]
        public ActionResult updateanh1(long? id, string anh_url)
        {
            try
            {
                var sql = "update products set product_photo = '" + anh_url + "' where product_id = " + id;
                var update = db.Database.ExecuteSqlCommand(sql);
            }
            catch
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult updateanh2(long? id, string anh_url)
        {
            try
            {
                var sql = "update products set product_photo2 = '" + anh_url + "' where product_id = " + id;
                var update = db.Database.ExecuteSqlCommand(sql);
            }
            catch
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return RedirectToRoute("Admin");
            }
            product _model = await db.products.FindAsync(id);
            if (_model == null)
            {
                return View(_model);
            }
            var getPro = new ProductVM()
            {
                cat_id = _model.cat_id,
                product_content = _model.product_content,
                product_id = _model.product_id,
                product_name = _model.product_name,
                product_new_type = _model.product_new_type,
                product_photo = _model.product_photo,
                product_photo2 = _model.product_photo2,
                product_price_public = _model.product_price_public,
                product_feature=_model.product_feature,
                product_technical=_model.product_technical,
                lang=_model.lang,
                //product_type = _model.product_type,
                status = (bool)_model.status,
                product_des = _model.product_des
            };

            ViewBag.TenCat = _model.product_name;
            return View(getPro);
        }


        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Errored"] = "Vui lòng kiểm tra lại các trường.";
                return RedirectToRoute("AdminEditProduct", new { id = model.product_id });
            }
            try
            {
                var _model = await db.products.FindAsync(model.product_id);
                if (_model != null)
                {
                    _model.cat_id = model.cat_id ?? null;
                    _model.product_name = model.product_name ?? null;
                    _model.product_content = model.product_content ?? null;
                    //_model.product_photo = model.product_photo ?? null;
                    //_model.product_photo2 = model.product_photo2 ?? null;
                    _model.product_price_public = model.product_price_public ?? null;
                    //_model.product_type = model.product_type ?? null;
                    _model.product_new_type = model.product_new_type ?? null;
                    _model.status = model.status;
                    _model.updated_date = DateTime.Now;
                    _model.product_des = model.product_des;
                    _model.lang = model.lang ?? null;
                    _model.product_feature = model.product_feature ?? null;
                    _model.product_technical = model.product_technical ?? null;
                    db.Entry(_model).State = System.Data.Entity.EntityState.Modified;
                    await db.SaveChangesAsync();
                    TempData["Updated"] = "Cập nhật sản phẩm thành công";
                }
            }
            catch (Exception ex)
            {
                TempData["Errored"] = "Có lỗi xảy ra khi cập nhật danh mục.";
                //configs.SaveTolog(ex.ToString());
                return RedirectToRoute("AdminEditProduct", new { id = model.product_id });
            }
            TempData["Errored"] = "Cập nhật thành công.";
            //configs.SaveTolog(ex.ToString());
            return RedirectToRoute("AdminEditProduct", new { id = model.product_id });

        }

        public ActionResult Delete(long? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToRoute("Admin");
            }
            product _model = db.products.Find(id);
            if (_model == null)
            {
                return View();
            }
            return View(_model);
        }
        //tailennhieuanh
        public ActionResult tailennhieuanh(long? product_id)
        {
            bool isSaved = true;
            int fName = 0;
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}images\\photos", Server.MapPath(@"\")));
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString());

                        var _fileName = Guid.NewGuid().ToString("N") + ".jpg";

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, _fileName);
                        //System.Drawing.Image bm = System.Drawing.Image.FromStream(file.InputStream);
                        // Thay đổi kích thước ảnh
                        //bm = ResizeBitmap((Bitmap)bm, 400, 310); /// new width, height
                        // Giảm dung lượng ảnh trước khi lưu
                        //ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                        //ImageCodecInfo ici = null;
                        //foreach (ImageCodecInfo codec in codecs)
                        //{
                        //    if (codec.MimeType == "image/jpeg")
                        //        ici = codec;
                        //}
                        //EncoderParameters ep = new EncoderParameters();
                        //ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)80);
                        //bm.Save(path, ici, ep);
                        //bm.Save(path);
                        file.SaveAs(path);
                        string file_url = "/images/photos/" + _fileName;
                        var update_img_product = db.Database.ExecuteSqlCommand("INSERT INTO product_img(img_url,product_id) VALUES('" + file_url + "'," + product_id + ")");
                        fName = 1;
                        //return Json(new { Message = file_url }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                isSaved = false;
                //Helpers.configs.SaveTolog(ex.ToString());
            }
            if (isSaved)
            {
                return Json(new { Message = fName }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Message = "Có lỗi khi lưu tệp tin" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long? id)
        {
            product _model = await db.products.FindAsync(id);
            if (_model == null)
            {
                return View();
            }
            if (_model.product_img.Count() > 0)
            {
                TempData["Error"] = "Bạn không thể xóa sản phẩm này. <br />";
                return RedirectToRoute("AdminDeleteProduct", new { id = _model.product_id });
            }
            try
            {
                db.products.Remove(_model);
                await db.SaveChangesAsync();
                TempData["Deleted"] = "Sản phẩm đã được xóa khỏi danh sách.";
            }
            catch (Exception ex)
            {
                //configs.SaveTolog(ex.ToString());
            }

            return RedirectToRoute("AdminListProduct");
        }
        public PartialViewResult _lstOptionCatPartial()
        {
            List<LstCat> data = db.cats.Select(x => new LstCat()
            {
                CatId = x.cat_id,
                CatName = x.cat_name,
                ParentCatId = x.cat_parent_id,
                CatPos = x.cat_pos,
                CatURL = x.cat_url
            }).OrderBy(x => x.CatPos).ToList();

            var presidents = data.Where(x => x.ParentCatId == null).FirstOrDefault();
            SetChildrenCat(presidents, data);
            return PartialView("_lstOptionCatPartial", presidents);
        }
        private void SetChildrenCat(LstCat model, List<LstCat> danhmuc)
        {
            var childs = danhmuc.Where(x => x.ParentCatId == model.CatId).ToList();
            if (childs.Count > 0)
            {
                foreach (var child in childs)
                {
                    SetChildrenCat(child, danhmuc);
                    model.LstCats.Add(child);
                }
            }
        }
        public ActionResult LoadPhotoProduct(long? id)
        {
            var model = db.products.Find(id).product_img.ToList();//db.products.Find(id).product_img.ToList();
            return PartialView("_LoadPhotoProduct", model);
        }
        
        public ActionResult LoadPhotoProduct2(long? id)
        {
            var model = db.products.Find(id).product_img.ToList();//db.products.Find(id).product_img.ToList();
            return PartialView("LoadPhotoProduct2", model);
        }
        public ActionResult SaveImage()
        {
            bool isSaved = true;
            var fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}images\\photos", Server.MapPath(@"\")));
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString());

                        var _fileName = Guid.NewGuid().ToString("N") + ".jpg";

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, _fileName);
                        System.Drawing.Image bm = System.Drawing.Image.FromStream(file.InputStream);
                        // Thay đổi kích thước ảnh
                        bm = ResizeBitmap((Bitmap)bm, 400, 310); /// new width, height
                        // Giảm dung lượng ảnh trước khi lưu
                        //ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                        //ImageCodecInfo ici = null;
                        //foreach (ImageCodecInfo codec in codecs)
                        //{
                        //    if (codec.MimeType == "image/jpeg")
                        //        ici = codec;
                        //}
                        //EncoderParameters ep = new EncoderParameters();
                        //ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)80);
                        //bm.Save(path, ici, ep);
                        bm.Save(path);
                        //file.SaveAs(path);
                        fName = "/images/photos/" + _fileName;
                    }
                }
            }
            catch (Exception ex)
            {
                isSaved = false;
                //Helpers.configs.SaveTolog(ex.ToString());
            }
            if (isSaved)
            {
                return Json(new { Message = fName }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Message = "Có lỗi khi lưu tệp tin" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveImageBig()
        {
            bool isSaved = true;
            var fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}images\\photos", Server.MapPath(@"\")));
                        //string strDay = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()+DateTime.Now.Day.ToString();
                        //string strDay = DateTime.Now.ToString("yyyyMM");
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString());

                        var _fileName = Guid.NewGuid().ToString("N") + ".jpg";

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, _fileName);
                        // System.Drawing.Image bm = System.Drawing.Image.FromStream(file.InputStream);
                        // Thay đổi kích cỡ
                        //bm = ResizeBitmap((Bitmap)bm, 1920, 790); /// new width, height
                        //// Giảm dung lượng ảnh trước khi lưu
                        //ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                        //ImageCodecInfo ici = null;
                        //foreach (ImageCodecInfo codec in codecs)
                        //{
                        //    if (codec.MimeType == "image/jpeg")
                        //        ici = codec;
                        //}
                        //EncoderParameters ep = new EncoderParameters();
                        //ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)80);
                        //bm.Save(path, ici, ep);
                        //bm.Save(path);
                        file.SaveAs(path);
                        fName = "/images/photos/" + _fileName;
                    }
                }
            }
            catch (Exception ex)
            {
                isSaved = false;
                //Helpers.configs.SaveTolog(ex.ToString());
            }
            if (isSaved)
            {
                return Json(new { Message = fName }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Message = "Có lỗi khi lưu tệp tin" }, JsonRequestBehavior.AllowGet);
            }
        }

        private Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((System.Drawing.Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
        }
        public ActionResult xoa_anh(long? id)
        {
            long? idproduct = 0;
            try
            {
                var photo = db.product_img.Find(id);
                if (photo != null)
                {
                    idproduct = photo.product_id;
                    db.product_img.Remove(photo);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //configs.SaveTolog(ex.ToString());
            }
            return RedirectToRoute("AdminEditProduct", new { id = idproduct });
        }


        //RestoreOffice       
        public async Task<ActionResult> Restore(long? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToRoute("Admin");
            }
            try
            {
                var _product = await db.products.FindAsync(id);
                if (_product != null)
                {
                    if (_product.status == false)
                    {
                        _product.status = true;
                        db.Entry(_product).State = System.Data.Entity.EntityState.Modified;
                        await db.SaveChangesAsync();

                        TempData["Updated"] = "Sản phẩm đã được khôi phục.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Errored"] = "Có lỗi xảy ra khi khôi phục.";
                //configs.SaveTolog(ex.ToString());

            }
            return RedirectToRoute("AdminEditProduct", new { id = id });
        }
        public ActionResult uploadfile(long? product_id,string name)
        {
            var fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}images\\files", Server.MapPath(@"\")));
                        string strDay = DateTime.Now.ToString("yyyyMM");
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), strDay);

                        var _fileName = Guid.NewGuid().ToString("N") + file.FileName.Substring(file.FileName.IndexOf("."));

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);
                    
                        fName =  "/images/files/" + strDay + "/" + _fileName;
                        file.SaveAs(Server.MapPath(@"\") + fName);
                        var update_img_product = db.Database.ExecuteSqlCommand("INSERT INTO product_file(title,file_path,product_id) VALUES('" + name + "','" + fName + "'," + product_id + ")");
                        fName = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                //configs.SaveTolog(ex.ToString());
            }
            return Json(new { Message = fName }, JsonRequestBehavior.AllowGet);
        }
    }
}