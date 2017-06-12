using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CADDiagramDrawing
{
    /// <summary>
    /// 列车在车站的作业
    /// </summary>
    public class TrainStationOp
    {
        /// <summary>
        /// 列车当前方向
        /// </summary>
        public Direction CurrentDirection { get; set; }
        /// <summary>
        /// 列车是否始发终到站
        /// </summary>
        public bool IsTerminal { get; set; }
        /// <summary>
        /// 所属车站
        /// </summary>
        public DStation Station { get; set; }
        /// <summary>
        /// 所属列车
        /// </summary>
        public DTrain Train { get; set; }
        /// <summary>
        /// 到达时刻
        /// </summary>
        public DateTime ArriveTime { get; set; }
        /// <summary>
        /// 出发时刻
        /// </summary>
        public DateTime DepartTime { get; set; }
        /// <summary>
        /// 到达时刻点横坐标
        /// </summary>
        internal double ArriveX { get; set; }
        /// <summary>
        /// 出发时刻点横坐标
        /// </summary>
        internal double DepartX { get; set; }
        /// <summary>
        /// 计算坐标
        /// </summary>
        internal void Calculate(double left,double XRatio)
        {
            ArriveX = left + XRatio * (ArriveTime.Second + ArriveTime.Minute * 60 + ArriveTime.Hour * 3600);
            DepartX = left + XRatio * (DepartTime.Second + DepartTime.Minute * 60 + DepartTime.Hour * 3600);
        }
    }
    /// <summary>
    /// 列车方向
    /// </summary>
    public enum Direction
    {
        Down,
        Up,
    }
}