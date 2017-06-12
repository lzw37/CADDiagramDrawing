﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CADDiagramDrawing
{
    /// <summary>
    /// CAD运行图
    /// </summary>
    public class CADDiagram
    {
        /// <summary>
        /// 运行图图名
        /// </summary>
        public string Title { get; set; } = "XX运行图";


        /// <summary>
        /// 图面高度，默认500
        /// </summary>
        public double Height { get; set; } = Parameters.DiagramHeight;

        /// <summary>
        /// 图面宽度，默认1440
        /// </summary>
        public double Width { get; set; } = Parameters.DiagramWidth;

        /// <summary>
        /// 图的四边留白
        /// </summary>
        public double Margin { get; set; } = Parameters.DiagramMargin;

        /// <summary>
        /// 单位时间（1s）在图上的长度
        /// </summary>
        internal double XRatio { get; set; }
        /// <summary>
        /// 单位里程（1km）在图上的长度
        /// </summary>
        internal double YRatio { get; set; }
        /// <summary>
        /// 区块中间的间隔
        /// </summary>
        public double BlockGap { get; set; } = Parameters.BlockGap;

        /// <summary>
        /// 列车集合
        /// </summary>
        public List<DTrain> TrainSet { get; set; } = new List<DTrain>();

        /// <summary>
        /// 运行图块集合
        /// </summary>
        public List<DiagramBlock> BlockSet { get; set; } = new List<DiagramBlock>();
        /// <summary>
        /// 时间线集合
        /// </summary>
        public List<DTime> TimeSet { get; set; } = new List<DTime>();

        /// <summary>
        /// 通过车站名获取车站对象
        /// </summary>
        public DStation GetStationByID(string id)
        {
            foreach (DiagramBlock b in BlockSet)
                foreach (DStation sta in b.DStationSet)
                {
                    if (sta.Id == id)
                        return sta;
                }
            throw new ApplicationException("找不到车站!");
        }

        /// <summary>
        /// 创建时间线
        /// </summary>
        public void CreateTime()
        {
            if (BlockSet.Count == 0)
                return;
            for (DateTime time = DateTime.Parse("00:00:00"); time <= DateTime.Parse("23:59:59"); time = time.AddMinutes(10))
            {
                DTime t = new DTime();
                t.Time = time;
                if (time.Minute == 30)/*半小时线*/
                {
                    t.Type = TimeLineType.HalfHour;
                }
                else if (time.Minute == 0)/*一小时线*/
                {
                    t.Type = TimeLineType.Hour;
                }
                else/*普通十分钟线*/
                {
                    t.Type = TimeLineType.Normal;
                }
                TimeSet.Add(t);
            }
        }

        /// <summary>
        /// 计算坐标
        /// </summary>
        public void Calculate()
        {
            /*计算总图的宽度*/
            double diagramWidth = Width - 2 * Margin;
            double diagramLeft = Margin;
            XRatio = diagramWidth / 86400;

            /*计算总图的高度*/
            double diagramHeight = Height - 2 * Margin;
            double diagramTop = Margin;
            double totalMileage = 0;/*总计图上里程*/
            foreach (DiagramBlock db in BlockSet)
            {
                totalMileage += db.DStationSet.Last().CenterMileage;
            }
            double YRatio = (Height - 2 * Margin - (BlockSet.Count - 1) * BlockGap) / totalMileage;


            /*计算各区块的车站线位置*/
            double currentTop = diagramTop;
            foreach (DiagramBlock db in BlockSet)
            {
                db.Calculate(diagramLeft, diagramWidth, currentTop, YRatio);
                currentTop += (db.Height + BlockGap);
            }

            /*计算时间线*/
            foreach (DTime t in TimeSet)
            {
                t.TopAndBottomSet.Clear();
                foreach (DiagramBlock block in BlockSet)
                {
                    t.CreateSectors(block.Top, block.Top + block.Height);
                }
                t.Calculate(diagramLeft, XRatio);
            }

            /*计算列车*/
            foreach (DTrain tr in TrainSet)
            {
                tr.TrainIDInsect += new TrainIDInsectEventHandler(CheckTrainIDInsect);
                tr.Calculate(diagramLeft, XRatio);
            }
        }

        /// <summary>
        /// 绘制运行图
        /// </summary>
        public void DrawDiagram()
        {
            DXFLibrary.Document doc = new DXFLibrary.Document();

            DXFLibrary.Tables tables = new DXFLibrary.Tables();
            doc.SetTables(tables);

            DXFLibrary.Table layers = new DXFLibrary.Table("LAYER");
            tables.addTable(layers);

            DrawStations(doc, layers);
            DrawTimeLine(doc, layers);
            DrawTrains(doc, layers);

            System.IO.FileStream fs = new System.IO.FileStream(this.Title + ".dxf", System.IO.FileMode.Create);
            DXFLibrary.Writer.Write(doc, fs);
            fs.Close();
        }
        /// <summary>
        /// 绘制车站线
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="layers"></param>
        private void DrawStations(DXFLibrary.Document doc, DXFLibrary.Table layers)
        {
            DXFLibrary.Layer layerStations;
            layerStations = new DXFLibrary.Layer("Stations", 84, "CONTINUOUS");
            layers.AddTableEntry(layerStations);

            DXFLibrary.Layer layerBlocks;
            layerBlocks = new DXFLibrary.Layer("Blocks", 84, "CONTINUOUS");
            layers.AddTableEntry(layerBlocks);

            /*绘制车站线*/
            foreach (DiagramBlock db in BlockSet)
            {
                db.Draw(doc);
            }
        }
        /// <summary>
        /// 绘制时间线
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="layers"></param>
        private void DrawTimeLine(DXFLibrary.Document doc, DXFLibrary.Table layers)
        {
            DXFLibrary.Layer layerTimes;
            layerTimes = new DXFLibrary.Layer("Times", 84, "CONTINUOUS");
            layers.AddTableEntry(layerTimes);

            DXFLibrary.Layer layerHalfTimes;
            layerHalfTimes = new DXFLibrary.Layer("HalfTimes", 84, "CONTINUOUS");
            layers.AddTableEntry(layerHalfTimes);

            DXFLibrary.Layer layerHourTimes;
            layerHourTimes = new DXFLibrary.Layer("HourTimes", 84, "CONTINUOUS");
            layers.AddTableEntry(layerHourTimes);

            /*绘制时间线*/
            foreach (DTime t in TimeSet)
            {
                t.Draw(doc);
            }
        }

        /// <summary>
        /// 绘制列车
        /// </summary>
        private void DrawTrains(DXFLibrary.Document doc, DXFLibrary.Table layers)
        {
            DXFLibrary.Layer layerTrains;
            layerTrains = new DXFLibrary.Layer("Trains", 10, "CONTINUOUS");
            layers.AddTableEntry(layerTrains);

            DXFLibrary.Layer layerTrainID;
            layerTrainID = new DXFLibrary.Layer("TrainID", 10, "CONTINUOUS");
            layers.AddTableEntry(layerTrainID);

            DXFLibrary.Layer layerTrainTime;
            layerTrainTime = new DXFLibrary.Layer("TrainTime", 10, "CONTINUOUS");
            layers.AddTableEntry(layerTrainTime);

            foreach (DTrain tr in TrainSet)
            {
                tr.Draw(doc);
            }
        }
        /// <summary>
        /// 判断当前车次与其他车次是否重合
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        private bool CheckTrainIDInsect(DTrain tr, RectangleF rect)
        {
            foreach(DTrain trCompare in TrainSet)
            {
                if (trCompare == tr)
                    continue;
                foreach(IDecorateLabel trLb in trCompare.TrainDecorateSet)
                {
                    if (trLb.GetType() != typeof(TrainIDLabel))
                        continue;
                    if (((TrainIDLabel)trLb).Rect.IntersectsWith(rect))
                        return true;
                }
            }
            return false;
        }
    }
}
