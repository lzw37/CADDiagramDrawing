﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CADDiagramDrawing
{
    /// <summary>
    /// 运行图区块
    /// </summary>
    public class DiagramBlock
    {
        /// <summary>
        /// DiagramBlock类的构造函数
        /// </summary>
        /// <param name="BlockID">运行图区块编号</param>
        public DiagramBlock(string BlockID)
        {
            ID = BlockID;
        }

        /// <summary>
        /// 区块编号
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 车站集合
        /// </summary>
        internal List<DStation> DStationSet { get; set; } = new List<DStation>();

        #region 坐标
        /// <summary>
        /// 计算坐标
        /// </summary>
        internal void Calculate(double left, double width, double top, double YRatio)
        {
            Left = left;
            Top = top;
            Width = width;
            foreach (DStation station in DStationSet)
            {
                station.Calculate(top, YRatio, left, left + width);
            }
            Height = DStationSet.Last().Y - Top;
        }

        /// <summary>
        /// 运行图区块左端
        /// </summary>
        internal double Left { get; set; }

        /// <summary>
        /// 运行图区块上端
        /// </summary>
        internal double Top { get; set; }

        /// <summary>
        /// 运行图区块高度
        /// </summary>
        internal double Height { get; set; }

        /// <summary>
        /// 运行图区块宽度
        /// </summary>
        internal double Width { get; set; }

        /// <summary>
        /// 绘制运行图区块
        /// </summary>
        /// <param name="doc">DXF文件对象</param>
        internal void Draw(DXFLibrary.Document doc)
        {
            /*绘制每个车站线*/
            foreach(DStation station in DStationSet)
            {
                station.Draw(doc);
            }

            /*绘制运行图区块的外框*/
            DXFLibrary.PolyLine l =new DXFLibrary.PolyLine ("Blocks");
            l.AddVertex(new DXFLibrary.Vertex (Left,-Top, "Blocks"));
            l.AddVertex(new DXFLibrary.Vertex(Left+Width, -Top, "Blocks"));
            l.AddVertex(new DXFLibrary.Vertex(Left+Width, -(Top+Height), "Blocks"));
            l.AddVertex(new DXFLibrary.Vertex(Left, -(Top+Height), "Blocks"));
            l.AddVertex(new DXFLibrary.Vertex(Left, -Top, "Blocks"));
            doc.add(l);
        }

        #endregion 坐标

    }
}