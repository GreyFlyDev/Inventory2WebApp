﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryWebApp.Models;
using Microsoft.AspNet.Identity;

namespace InventoryWebApp.Controllers
{
    [Authorize]
    public class RestocksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Restocks
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var restocks = db.Restocks.Where(r => r.UserId == userId);
            return View(db.Restocks.ToList());
        }

        // GET: Restocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restock restock = db.Restocks.Find(id);
            if (restock == null)
            {
                return HttpNotFound();
            }
            return View(restock);
        }

        // GET: Restocks/Create
        public ActionResult Create(int id)
        {
            TempData["ProductId"] = id;
            return View();
        }

        // POST: Restocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RestockId,Quantity,TotalCost,Date,ProductId,UserId")] Restock restock)
        {
            if (ModelState.IsValid)
            {
                int productId = (int)TempData["ProductId"];
                var product = db.Products.Where(p => p.ProductId == productId).FirstOrDefault();

                restock.Date = DateTime.Now;
                restock.UserId = User.Identity.GetUserId().ToString();
                restock.ProductId = productId;

                db.Restocks.Add(restock);
                db.SaveChanges();

                product.Quantity += restock.Quantity;
                product.TotalInvestment += restock.TotalCost;
                db.SaveChanges();

                product.PurchasePricePerUnit = product.TotalInvestment / product.Quantity;
                if (product.PurchasePricePerUnit < 0)
                    product.PurchasePricePerUnit = 0;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restock);
        }

        // GET: Restocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restock restock = db.Restocks.Find(id);
            if (restock == null)
            {
                return HttpNotFound();
            }
            return View(restock);
        }

        // POST: Restocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RestockId,Quantity,TotalCost,Date,ProductId,UserId")] Restock restock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restock);
        }

        // GET: Restocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restock restock = db.Restocks.Find(id);
            if (restock == null)
            {
                return HttpNotFound();
            }
            return View(restock);
        }

        // POST: Restocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restock restock = db.Restocks.Find(id);
            db.Restocks.Remove(restock);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
