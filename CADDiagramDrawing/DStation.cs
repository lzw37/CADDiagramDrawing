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
        /// <summary>
        /// 车站类的构造函数
        /// </summary>
        /// <param name="id">车站ID</param>
        /// <param name="name">车站名</param>
        /// <param name="parentBlock">所属区块</param>
        /// <param name="centerMileage">所在区块的车站里程（距区块顶端的相对位置）</param>
        public DStation(string id, string name, DiagramBlock parentBlock, double centerMileage)
        {
            Id = id;
            Name = name;
            ParentBlock = parentBlock;
            CenterMileage = centerMileage;
            ParentBlock.DStationSet.Add(this);/*将当前车站线对象加入区块中*/
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
        internal DiagramBlock ParentBlock { get; set; }

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
        internal void Calculate(double blockTop, double YRatio, double left, double right)
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
        internal void Draw(DXFLibrary.Document doc)
        {
            DXFLibrary.Line l = new DXFLibrary.Line("Stations", Left, -Y, Right, -Y);
            doc.add(l);
        }
    }
}