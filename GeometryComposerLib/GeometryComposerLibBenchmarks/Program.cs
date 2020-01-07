namespace GeometryComposerLibBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            Samples.Computers.Voronoi.Sample1.Execute();

            //Use this for benchmarking
            //var summary = BenchmarkRunner.Run<Benchmarks.BasicOperations.BasicOperationsBenchmark1>();

            //var benchmark = new Benchmarks.BasicOperations.BasicOperationsBenchmark1();

            ////benchmark.Validate(10);
            //var maxDiff = benchmark.Validate(10);

            //Console.Out.WriteLine("Max Absolute Component Difference: " + maxDiff.ToString("G"));
            //Console.Out.WriteLine("");

            //Console.Out.WriteLine("Press 'Enter' to exit...");
            //Console.In.ReadLine();

            ////Use this for executing the main benchmarking methods without benchmarking
            //var benchmark = new AcceleratorsBenchmark1()
            //{
            //    LineSegmentsCount = 190,
            //    UseLimitedLines = true
            //};

            //benchmark.Setup();

            //benchmark.TestIntersectionFromBih();

            //var i = 1;
            //foreach (var line in benchmark.LinesList)
            //{
            //    var drawingBoard = benchmark
            //        .LineSegmentsList
            //        .GetBoundingBox2D()
            //        .CreateDrawingBoard(1);

            //    drawingBoard.ActiveLayer.SetPen(2, Color.Black);

            //    foreach (var lineSegment in benchmark.LineSegmentsList)
            //        drawingBoard.ActiveLayer.DrawLineSegment(lineSegment);

            //    drawingBoard.ActiveLayer.SetPen(2, Color.Coral);
            //    drawingBoard.ActiveLayer.DrawLine(line);

            //    drawingBoard.ActiveLayer.SetPen(2, Color.Brown);
            //    drawingBoard.ActiveLayer.DrawLineSegment(line.GetLineSegment());

            //    drawingBoard.SaveToPngFile(
            //        Path.Combine(Directory.GetCurrentDirectory(), "Lines-" + i + ".png")
            //    );

            //    i++;
            //}

            //var composer = new LinearComposer();

            //composer
            //    .AppendLineAtNewLine(benchmark.CompareGetAllIntersections())
            //    .AppendLineAtNewLine(benchmark.CompareGetEdgeIntersections())
            //    .AppendLineAtNewLine(benchmark.CompareGetFirstIntersection())
            //    .AppendLineAtNewLine(benchmark.CompareGetLastIntersection())
            //    .AppendLineAtNewLine(benchmark.CompareTestIntersection());

            //Console.Out.WriteLine(composer.ToString());


            ////Use this for samples and testing
            //Sample4.Execute();

            //Console.Out.WriteLine("Press 'Enter' to exit...");
            //Console.In.ReadLine();
        }
    }
}
