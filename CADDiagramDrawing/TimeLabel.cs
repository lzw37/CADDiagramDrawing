using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXFLibrary;

namespace CADDiagramDrawing
{
    /// <summary>
    /// 到发时间点数字
    /// </summary>
    internal class TimeLabel
    {
        /// <summary>
        /// 时间点所描述的到发作业对象
        /// </summary>
        internal TrainStationOp ParentOperation { get; set; }
        /// <summary>
        /// 时间点数字高
        /// </summary>
        internal double TextHeight { get; set; } = Parameters.TimeTextHeight;
        /// <summary>
        /// 绘制时间点数字
        /// </summary>
        /// <param name="doc"></param>
        internal void Draw(Document doc)
        {
            if (!(ParentOperation.ArriveTime == ParentOperation.DepartTime))
            {
                DrawArrive(doc);
            }
            DrawDepart(doc);
        }
        /// <summary>
        /// 绘制到达时刻数字
        /// </summary>
        /// <param name="doc"></param>
        private void DrawArrive(Document doc)
        {
            if (ParentOperation.CurrentDirection == Direction.Down)
            {
                DXFLibrary.Text t = new Text((ParentOperation.ArriveTime.Minute % 10).ToString(),
                    ParentOperation.ArriveX + Parameters.TimeExcurtion,
                    -(ParentOperation.Station.Y - Parameters.TimeExcurtion),
                    TextHeight, "TrainTime");
                doc.add(t);
                if (ParentOperation.ArriveTime.Second != 0)
                {
                    DXFLibrary.Text ts = new Text(ParentOperation.ArriveTime.Second.ToString(),
                        (ParentOperation.ArriveX + TextHeight / 1.5) + Parameters.TimeExcurtion,
                        -(ParentOperation.Station.Y - Parameters.TimeExcurtion),
                        TextHeight / 3, "TrainTime");
                    doc.add(ts);
                }
            }
            else
            {
                DXFLibrary.Text t = new Text((ParentOperation.ArriveTime.Minute % 10).ToString(),
                    ParentOperation.ArriveX + Parameters.TimeExcurtion,
                    -(ParentOperation.Station.Y + TextHeight + Parameters.TimeExcurtion),
                    TextHeight, "TrainTime");
                doc.add(t);
                if (ParentOperation.ArriveTime.Second != 0)
                {
                    DXFLibrary.Text ts = new Text(ParentOperation.ArriveTime.Second.ToString(),
                        (ParentOperation.ArriveX + Parameters.TimeTextWidth) + Parameters.TimeExcurtion,
                        -(ParentOperation.Station.Y + TextHeight + Parameters.TimeExcurtion),
                        TextHeight / 3, "TrainTime");
                    doc.add(ts);
                }
            }
        }
        /// <summary>
        /// 绘制出发时刻数字
        /// </summary>
        /// <param name="doc"></param>
        private void DrawDepart(Document doc)
        {
            if (ParentOperation.CurrentDirection == Direction.Down)
            {
                DXFLibrary.Text t = new Text((ParentOperation.DepartTime.Minute % 10).ToString(),
                    ParentOperation.DepartX - Parameters.TimeTextWidth * 2 + Parameters.TimeExcurtion,
                    -(ParentOperation.Station.Y + TextHeight + Parameters.TimeExcurtion),
                    TextHeight, "TrainTime");
                doc.add(t);
                if (ParentOperation.DepartTime.Second != 0)
                {
                    DXFLibrary.Text ts = new Text(ParentOperation.DepartTime.Second.ToString(),
                        (ParentOperation.DepartX - Parameters.TimeTextWidth) + Parameters.TimeExcurtion,
                        -(ParentOperation.Station.Y + TextHeight + Parameters.TimeExcurtion),
                        TextHeight / 3, "TrainTime");
                    doc.add(ts);
                }
            }
            else
            {
                DXFLibrary.Text t = new Text((ParentOperation.DepartTime.Minute % 10).ToString(),
                    ParentOperation.DepartX - Parameters.TimeTextWidth * 2 + Parameters.TimeExcurtion,
                    -(ParentOperation.Station.Y - Parameters.TimeExcurtion),
                    TextHeight, "TrainTime");
                doc.add(t);
                if (ParentOperation.DepartTime.Second != 0)
                {
                    DXFLibrary.Text ts = new Text(ParentOperation.DepartTime.Second.ToString(),
                        (ParentOperation.DepartX -Parameters.TimeTextWidth) + Parameters.TimeExcurtion,
                        -(ParentOperation.Station.Y - Parameters.TimeExcurtion),
                        TextHeight / 3, "TrainTime");
                    doc.add(ts);
                }
            }
        }
    }
}