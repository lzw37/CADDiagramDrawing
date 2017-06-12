using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CADDiagramDrawing
{
    internal interface IDecorateLabel
    {
        void Draw(DXFLibrary.Document doc);
    }
}