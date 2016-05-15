using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;

/// <summary>
/// Created at 15.05.2016 by EM Dobre.
/// You can add, modify, copy ... do what you want. ;)
/// AutoCAD 2015 tool to create 2D/3D polylines from files.
/// The user select the file that contains the coordonates 
/// and the tool reads the coordinates and create the polyline.
/// The coordinates should be writes with one space between them, ex: x y z
/// If there are more lines, there shold be one row space between lines coordinate.
////// </summary>

namespace PLineFile
{
    public class Class1
    {
        //Command method. The command that you should run from command line.
        /// <summary>
        /// AutoCAD command to run the tool: CreatePlines
        /// </summary>
        [CommandMethod("CreatePlines")]
        public void CreatePlines()
        {
            //get document object to access the database of the document
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acDB = acDoc.Database;

            OpenFile selectedFile = new OpenFile();
            selectedFile.SelectFile();
            string file = selectedFile.SelectedFileName;

            Application.ShowAlertDialog(file);          
           
        }



       
    }
}
