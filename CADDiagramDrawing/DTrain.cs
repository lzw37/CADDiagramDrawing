using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CADDiagramDrawing
{
    /// <summary>
    /// 列车
    /// </summary>
    public class DTrain
    {
        /// <summary>
        /// 车次
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 车站到发时刻集合
        /// </summary>
        public List<TrainStationOp> TrainStationOpSet { get; set; } = new List<TrainStationOp>();

        /// <summary>
        /// 车次接入、交出标志集合
        /// </summary>
        internal List<IDecorateLabel> TrainDecorateSet { get; set; } = new List<IDecorateLabel>();

        /// <summary>
        /// 时间标志集合
        /// </summary>
        internal List<TimeLabel> TimeLabelSet { get; set; } = new List<TimeLabel>();

        /// <summary>
        /// 判断列车车次是否相交的委托
        /// </summary>
        internal TrainIDInsectEventHandler TrainIDInsect;

        /// <summary>
        /// 绘制列车运行线
        /// </summary>
        /// <param name="doc">DXF文件对象</param>
        internal void Draw(DXFLibrary.Document doc)
        {
            double lastX = 0;
            double lastY = 0;
            TrainStationOp lastTso = null;
            foreach(TrainStationOp tso in TrainStationOpSet)
            {
                if (lastTso==null || lastTso.Station.ParentBlock!=tso.Station.ParentBlock)/*列车径路的第一个车站，或某区块的第一个车站*/
                {
                    /*不绘制区间运行线*/
                }
                else
                {
                    /*绘制区间运行线*/
                    DXFLibrary.Line l1 = new DXFLibrary.Line("Trains", lastX, -lastY, tso.ArriveX, -tso.Station.Y);
                    doc.add(l1);
                }
                lastX = tso.ArriveX;
                lastY = tso.Station.Y;

                /*绘制车站运行线*/
                DXFLibrary.Line l2 = new DXFLibrary.Line("Trains", lastX, -lastY, tso.DepartX, -tso.Station.Y);
                doc.add(l2);
                lastX = tso.DepartX;
                lastY = tso.Station.Y;


                lastTso = tso;
            }

            /*画接入交出标记*/
            foreach(IDecorateLabel dec in TrainDecorateSet)
            {
                dec.Draw(doc);
            }

            /*画时间标志*/
            foreach (TimeLabel tlb in TimeLabelSet)
            {
                tlb.Draw(doc);
            }
        }

        /// <summary>
        /// 计算坐标
        /// </summary>
        /// <param name="diagramLeft">运行图左侧坐标</param>
        /// <param name="XRatio">X左边换算比例</param>
        internal void Calculate(double diagramLeft, double XRatio)
        {
            TrainStationOp lastTso = null;
            foreach (TrainStationOp tso in TrainStationOpSet)
            {
                tso.Calculate(diagramLeft, XRatio);/*计算各作业时间点的坐标*/

                TimeLabel tlb = new TimeLabel();/*时间标记对象*/
                tlb.ParentOperation = tso;
                TimeLabelSet.Add(tlb);


                if (lastTso == null || lastTso.Station.ParentBlock != tso.Station.ParentBlock)/*为列车的始发站、区块首站绘制接入标志*/
                {
                    /*交出标志*/
                    if (lastTso != null)
                    {
                        TrainEndLabel trEndLb = new TrainEndLabel();
                        trEndLb.IsTerminal = lastTso.IsTerminal;
                        trEndLb.CurrentDirection = lastTso.CurrentDirection;
                        trEndLb.Calculate(lastTso.DepartX, lastTso.Station.Y);
                        TrainDecorateSet.Add(trEndLb);
                    }

                    /*接入标志*/
                    TrainIDLabel trID = new TrainIDLabel();
                    trID.TrainName = this.Name;
                    trID.IsTerminal = tso.IsTerminal;
                    trID.CurrentDirection = tso.CurrentDirection;
                    trID.Calculate(tso.ArriveX, tso.Station.Y);

                    /*判断当前车次框与之前的车次框是否有相交，若有则调整车次框的位置，直至不相交为止*/
                    while (TrainIDInsect.Invoke(this,trID.Rect))
                    {
                        trID.AdjustTrainIDPosition();
                    }
                    TrainDecorateSet.Add(trID);
                }
                lastTso = tso;
            }

            /*列车在图上的最后一个车站，绘制交出或终到标志*/
            TrainEndLabel finalTrEndLb = new TrainEndLabel();
            finalTrEndLb.IsTerminal = lastTso.IsTerminal;
            finalTrEndLb.CurrentDirection = lastTso.CurrentDirection;
            finalTrEndLb.Calculate(lastTso.DepartX, lastTso.Station.Y);
            TrainDecorateSet.Add(finalTrEndLb);
        }
    }
}