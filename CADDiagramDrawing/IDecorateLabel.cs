using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CADDiagramDrawing
{
    /// <summary>
    /// 列车接入（始发）交出（终到）标记
    /// </summary>
    internal interface IDecorateLabel
    {
        void Draw(DXFLibrary.Document doc);
    }
}