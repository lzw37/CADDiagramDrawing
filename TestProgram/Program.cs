using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CADDiagramDrawing;

namespace TestProgram
{
    class Program
    {

        static CADDiagram diagram;
        static void Main(string[] args)
        {
            /*创建运行图对象*/
            diagram = new CADDiagram();

            /*设置运行图名*/
            diagram.Title = "京津城际铁路列车运行图";

            /*设置运行图尺寸*/
            diagram.Height = 500;
            diagram.Width = 1440;

            /*创建车站线*/
            CreateStationView();

            /*创建时间线*/
            diagram.CreateTime();

            /*创建列车*/
            CreateTrainView();

            /*计算运行图坐标*/
            diagram.Calculate();

            /*输出运行图*/
            diagram.DrawDiagram();
        }
        /// <summary>
        /// 创建车站线
        /// </summary>
        static void CreateStationView()
        {
            /*创建运行图区块*/
            DiagramBlock block1 = new DiagramBlock("0");
            diagram.BlockSet.Add(block1);

            /*创建车站*/
            DStation station1 = new DStation("1", "车站1", block1, 0);
            DStation station2 = new DStation("2", "车站2", block1, 21.3);
            DStation station3 = new DStation("3", "车站3", block1, 45.6);

            
            DiagramBlock block2 = new DiagramBlock("1");
            diagram.BlockSet.Add(block2);

            DStation station4 = new DStation("4", "车站4", block2, 0);
            DStation station5 = new DStation("5", "车站5", block2, 37.7);
            DStation station6 = new DStation("6", "车站6", block2, 61.3);


            DiagramBlock block3 = new DiagramBlock("2");
            diagram.BlockSet.Add(block3);

            DStation station7 = new DStation("7", "车站7", block3, 0);
            DStation station8 = new DStation("8", "车站8", block3, 10.1);
            DStation station9 = new DStation("9", "车站9", block3, 23.6);
        }
        /// <summary>
        /// 创建列车
        /// </summary>
        static void CreateTrainView()
        {
            DTrain train = new DTrain();
            train.Name = "G1001";
            TrainStationOp tso = new TrainStationOp();
            tso.Train = train;
            tso.IsTerminal = true;
            tso.Station = diagram.GetStationByName("1");
            tso.ArriveTime = DateTime.Parse("05:00:00");
            tso.DepartTime = DateTime.Parse("05:05:00");
            tso.CurrentDirection = Direction.Down;
            train.TrainStationOpSet.Add(tso);

            tso = new TrainStationOp();
            tso.Train = train;
            tso.Station = diagram.GetStationByName("2");
            tso.ArriveTime = DateTime.Parse("05:30:00");
            tso.DepartTime = DateTime.Parse("05:35:00");
            tso.CurrentDirection = Direction.Down;
            train.TrainStationOpSet.Add(tso);

            tso = new TrainStationOp();
            tso.Train = train;
            tso.Station = diagram.GetStationByName("3");
            tso.ArriveTime = DateTime.Parse("05:50:00");
            tso.DepartTime = DateTime.Parse("05:55:00");
            tso.CurrentDirection = Direction.Down;
            train.TrainStationOpSet.Add(tso);

            tso = new TrainStationOp();
            tso.Train = train;
            tso.Station = diagram.GetStationByName("4");
            tso.ArriveTime = DateTime.Parse("06:15:20");
            tso.DepartTime = DateTime.Parse("06:18:30");
            tso.CurrentDirection = Direction.Down;
            train.TrainStationOpSet.Add(tso);

            tso = new TrainStationOp();
            tso.Train = train;
            tso.IsTerminal = true;
            tso.Station = diagram.GetStationByName("5");
            tso.ArriveTime = DateTime.Parse("06:32:15");
            tso.DepartTime = DateTime.Parse("06:34:20");
            tso.CurrentDirection = Direction.Down;
            train.TrainStationOpSet.Add(tso);

            diagram.TrainSet.Add(train);




            train = new DTrain();
            train.Name = "G1002";
            tso = new TrainStationOp();
            tso.Train = train;
            tso.IsTerminal = true;
            tso.Station = diagram.GetStationByName("9");
            tso.ArriveTime = DateTime.Parse("05:00:00");
            tso.DepartTime = DateTime.Parse("05:05:00");
            tso.CurrentDirection = Direction.Up;
            train.TrainStationOpSet.Add(tso);

            tso = new TrainStationOp();
            tso.Train = train;
            tso.Station = diagram.GetStationByName("8");
            tso.ArriveTime = DateTime.Parse("05:30:00");
            tso.DepartTime = DateTime.Parse("05:35:00");
            tso.CurrentDirection = Direction.Up;
            train.TrainStationOpSet.Add(tso);

            tso = new TrainStationOp();
            tso.Train = train;
            tso.Station = diagram.GetStationByName("7");
            tso.ArriveTime = DateTime.Parse("05:50:00");
            tso.DepartTime = DateTime.Parse("05:55:00");
            tso.CurrentDirection = Direction.Up;
            train.TrainStationOpSet.Add(tso);

            tso = new TrainStationOp();
            tso.Train = train;
            tso.Station = diagram.GetStationByName("6");
            tso.ArriveTime = DateTime.Parse("06:15:20");
            tso.DepartTime = DateTime.Parse("06:18:30");
            tso.CurrentDirection = Direction.Up;
            train.TrainStationOpSet.Add(tso);

            tso = new TrainStationOp();
            tso.Train = train;
            tso.Station = diagram.GetStationByName("5");
            tso.ArriveTime = DateTime.Parse("06:32:15");
            tso.DepartTime = DateTime.Parse("06:34:20");
            tso.CurrentDirection = Direction.Up;
            train.TrainStationOpSet.Add(tso);

            tso = new TrainStationOp();
            tso.Train = train;
            tso.Station = diagram.GetStationByName("4");
            tso.ArriveTime = DateTime.Parse("06:50:00");
            tso.DepartTime = DateTime.Parse("06:55:00");
            tso.CurrentDirection = Direction.Up;
            train.TrainStationOpSet.Add(tso);

            tso = new TrainStationOp();
            tso.Train = train;
            tso.Station = diagram.GetStationByName("3");
            tso.ArriveTime = DateTime.Parse("07:20:20");
            tso.DepartTime = DateTime.Parse("07:24:30");
            tso.CurrentDirection = Direction.Up;
            train.TrainStationOpSet.Add(tso);

            tso = new TrainStationOp();
            tso.Train = train;
            tso.IsTerminal = true;
            tso.Station = diagram.GetStationByName("2");
            tso.ArriveTime = DateTime.Parse("07:33:15");
            tso.DepartTime = DateTime.Parse("07:35:20");
            tso.CurrentDirection = Direction.Up;
            train.TrainStationOpSet.Add(tso);

            diagram.TrainSet.Add(train);

        }

    }
}
