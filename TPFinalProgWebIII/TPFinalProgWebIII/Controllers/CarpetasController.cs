﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPFinalProgWebIII.Models.View;

namespace TPFinalProgWebIII.Controllers
{
    public class CarpetasController : CustomController
    {

        PW3TP_20181C_TareasEntities db = new PW3TP_20181C_TareasEntities();
        // GET: Carpetas
        public ActionResult Index()
        {
            int id;
            int.TryParse(Session["IdUsuario"].ToString(), out id);
            Usuario usuario = db.Usuario.Single(x => x.IdUsuario == id);

            ViewBag.carpetas = usuario.Carpeta.OrderBy(x => x.Nombre).ToList();

            return View("Index", ViewBag);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Crear-Carpeta")]
        public ActionResult Crear(CarpetaVal carpetaVal)
        {
            int id;

            if (!ModelState.IsValid)
            {
                return View();

            } else {

                Carpeta carpeta = new Carpeta();
                int.TryParse(Session["IdUsuario"].ToString(), out id);
                carpeta.IdUsuario = id;
                carpeta.Nombre = carpetaVal.Nombre;
                carpeta.Descripcion = carpetaVal.Descripcion;
                carpeta.Usuario = db.Usuario.Find(id);

                db.Carpeta.Add(carpeta);

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            
        }

        public ActionResult Tareas(int id)
        {
            ViewBag.tareas = db.Tarea.Where(x => x.IdCarpeta == id).ToList();

            return View("Tareas", ViewBag);
        }
    }
}