using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using System.IO;

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
            double x;
            double y;
            double z;
            char[] splitChar = { ' ' };
            string[] strArr = null;
            Point3dCollection points = new Point3dCollection();
            

            OpenFile selectedFile = new OpenFile();
            selectedFile.SelectFile();

            //read all lines into array
            Array inputLines = ArrLines(selectedFile.SelectedFileName);

            //cheack if array of line is empty
            if (inputLines == null)
            {
                Application.ShowAlertDialog("Selected file is empty");
                return;
            }

            //
            foreach (string line in inputLines)
            {
                if (line != "" || line == null)
                {
                    //split string at space and
                    //get the x,y,z coordiantes
                    strArr = line.Split(splitChar);
                    x = Convert.ToDouble(strArr[0]);
                    y = Convert.ToDouble(strArr[1]);
                    z = Convert.ToDouble(strArr[2]);

                    points.Add(new Point3d(x, y, z));

                }
                else
                {
                    CreatePolyLine(points);
                    points = new Point3dCollection();

                }
            }
        }


        /// <summary>
        /// method used to creat the 3DPolyLine
        /// </summary>
        /// <param name="points">Colection of 3D points </param>
        private void CreatePolyLine(Point3dCollection points)
        {

            Polyline3d poly = new Polyline3d();

            //get document object to access the database of the document
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acDB = acDoc.Database;

            try
            {
                //if line == "" or line==null draw thw polyline and pst3D=null
                using (Transaction trans = acDB.TransactionManager.StartTransaction())
                {

                    BlockTable acBlkTbl = (BlockTable)trans.GetObject(acDB.BlockTableId, OpenMode.ForRead, false);
                    BlockTableRecord acBlkTblRec = (BlockTableRecord)trans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite, true);

                    acBlkTblRec.AppendEntity(poly);
                    trans.AddNewlyCreatedDBObject(poly, true);
                    foreach (Point3d pt in points)
                    {
                        PolylineVertex3d vex3D = new PolylineVertex3d(pt);
                        poly.AppendVertex(vex3D);
                        trans.AddNewlyCreatedDBObject(vex3D, true);
                    }
                    poly.Closed = false;
                    // poly.ColorIndex = 1;
                   

                    trans.Commit();
                }
            }
            catch (System.Exception ex)
            {

                Application.ShowAlertDialog(ex.Message);
            }
        }

        /// <summary>
        /// Open the file and read all line into an array
        /// </summary>
        /// <param name="fileName"> FIle name</param>
        /// <returns></returns>
        private Array ArrLines(string fileName)
        {
            string[] lines = null;
            try
            {
                //check if file exists and read all lines
                if (File.Exists(fileName))
                {
                    lines = File.ReadAllLines(fileName);
                }
                else
                {
                    Application.ShowAlertDialog("File does not exists!");
                }
            }
            catch (System.Exception ex)
            {
                Application.ShowAlertDialog("Error: " + ex.Message);
            }

            return lines;
        }
    }
}
