using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CADDiagramDrawing
{
    /// <summary>
    /// 车站类
    /// </summary>
    public class DStation
    {
        public DStation(string id, string name, DiagramBlock parentBlock, double centerMileage)
        {
            Id = id;
            Name = name;
            ParentBlock = parentBlock;
            CenterMileage = centerMileage;
            ParentBlock.DStationSet.Add(this);
        }
        /// <summary>
        /// 车站名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 车站序号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父区块
        /// </summary>
        public DiagramBlock ParentBlock { get; set; }

        /// <summary>
        /// 车站中心里程
        /// </summary>
        public double CenterMileage { get; set; }

        /// <summary>
        /// 计算坐标
        /// </summary>
        /// <param name="blockTop">区块顶端坐标</param>
        /// <param name="YRatio">单位里程长度</param>
        /// <param name="left">区块左端坐标</param>
        /// <param name="right">区块右端坐标</param>
        public void Calculate(double blockTop, double YRatio, double left, double right)
        {
            Y = YRatio * CenterMileage + blockTop;
            Left = left;
            Right = right;
        }
        /// <summary>
        /// 车站线纵坐标
        /// </summary>
        internal double Y { get; set; }
        /// <summary>
        /// 车站线左端坐标
        /// </summary>
        internal double Left { get; set; }
        /// <summary>
        /// 车站线右端坐标
        /// </summary>
        internal double Right { get; set; }
        /// <summary>
        /// 绘制车站线
        /// </summary>
        /// <param name="doc"></param>
        public void Draw(DXFLibrary.Document doc)
        {
            DXFLibrary.Line l = new DXFLibrary.Line("Stations", Left, -Y, Right, -Y);
            doc.add(l);
        }
    }
}