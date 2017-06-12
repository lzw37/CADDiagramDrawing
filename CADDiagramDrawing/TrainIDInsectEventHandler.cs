using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CADDiagramDrawing
{
    /// <summary>
    /// 委托，用于判断当前车次框是否与其他车次框重合
    /// </summary>
    /// <param name="tr"></param>
    /// <param name="rect"></param>
    /// <returns></returns>
    public delegate bool TrainIDInsectEventHandler(DTrain tr, RectangleF rect);
}