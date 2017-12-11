using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.Tests.Infrastructure;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class CircularCloudLayouterShould
    {
        [SetUp]
        public void SetUp()
        {
            _testContext = new CircularCloudLayouterTestContext();
        }

        private CircularCloudLayouterTestContext _testContext;

        [TestCase(11, 5, 200, 0.70)]
        [TestCase(111, 5, 200, 0.70)]
        [TestCase(1111, 5, 200, 0.70)]
        [TestCase(211, 5, 200, 0.70)]
        [TestCase(11, 20, 200, 0.6)]
        [TestCase(11, 100, 100, 0.55)]
        [TestCase(11, 100, 20, 0.5)]
        public void GoodQuality_WhenPseudoRandom(int seed, int maxSize, int interations, double minLevel)
        {
            var rand = new Random(seed);
            var center = new Point(1024, 1024);
            _testContext.Init(center);

            for (var i = 0; i < interations; i++)
                _testContext.PutNextRectangle(new Size(rand.Next(2, maxSize), rand.Next(2, maxSize)));


            _testContext.GetQuality().Should().BeGreaterThan(minLevel);
        }

        [TestCase(11, 5, 200, 0.65)]
        [TestCase(111, 5, 200, 0.65)]
        [TestCase(1111, 5, 200, 0.65)]
        [TestCase(211, 5, 200, 0.65)]
        [TestCase(11, 20, 200, 0.55)]
        [TestCase(11, 100, 100, 0.50)]
        [TestCase(11, 100, 20, 0.45)]
        public void GoodQuality_WhenPseudoRandomWide(int seed, int maxSize, int interations, double minLevel)
        {
            var rand = new Random(seed);
            var center = new Point(1024, 1024);
            _testContext.Init(center);

            for (var i = 0; i < interations; i++)
                _testContext.PutNextRectangle(new Size(rand.Next(2, maxSize) * 2, rand.Next(2, maxSize)));
            _testContext.GetQuality().Should().BeGreaterThan(minLevel);
        }

        private static void AssertIntersection(CircularCloudLayouterTestContext testContext)
        {
            var generated = testContext.Generated;
            for (var i = 0; i < generated.Count - 1; i++) { 
            for (var j = i + 1; j < generated.Count; j++)
                if (generated[i].IntersectsWith(generated[j]))
                    Assert.Fail("index1:{0} rect1:{1}" +
                                "\n index2:{2} rect2:{3} intersected", i, generated[i], j,
                        generated[j]);
            }
        }

        [Test]
        public void NotIntersected_WhenManyInGreaterOrder()
        {
            var center = new Point(700, 900);
            _testContext.Init(center);

            for (var i = 0; i < 10; i++)
                _testContext.PutNextRectangle(new Size(80 + i * 5, 100 + i * 5));


            AssertIntersection(_testContext);
        }

        [Test]
        public void NotIntersected_WhenManyInSmallOrder()
        {
            var center = new Point(300, 200);
            _testContext.Init(center);

            for (var i = 0; i < 10; i++)
                _testContext.PutNextRectangle(new Size(80 - i * 5, 100 - i * 5));


            AssertIntersection(_testContext);
        }

        [Test]
        public void NotIntersected_WhenManySame()
        {
            var center = new Point(300, 200);
            _testContext.Init(center);

            for (var i = 0; i < 50; i++)
                _testContext.PutNextRectangle(new Size(8, 10));

            AssertIntersection(_testContext);
        }

        [Test]
        public void NotIntersected_WhenPseudoRandom()
        {
            var rand = new Random(10);
            var center = new Point(1024, 1024);
            _testContext.Init(center);

            for (var i = 0; i < 50; i++)
                _testContext.PutNextRectangle(new Size(rand.Next(2, 50), rand.Next(2, 50)));
            for (var i = 0; i < 50; i++)
                _testContext.PutNextRectangle(new Size(rand.Next(2, 20), rand.Next(2, 20)));


            AssertIntersection(_testContext);
        }

        [Test]
        [Timeout(1000)]
        public void NotTimeout_WhenPseudoRandom()
        {
            var rand = new Random(10);
            var center = new Point(1024, 1024);
            _testContext.Init(center);

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 50; j++)
                    _testContext.PutNextRectangle(new Size(rand.Next(2, 50), rand.Next(2, 50)));
                for (var j = 0; j < 50; j++)
                    _testContext.PutNextRectangle(new Size(rand.Next(2, 20), rand.Next(2, 20)));
            }
        }

        [Test]
        public void PlaceCenter_WhenSingle()
        {
            var center = new Point(20, 30);
            _testContext.Init(center);

            var rect = _testContext.PutNextRectangle(new Size(10, 12));
            new {rect.X, rect.Y, rect.Width, rect.Height}
                .ShouldBeEquivalentTo(
                    new {X = 15, Y = 24, Width = 10, Height = 12}
                );
        }

        [Test]
        public void PlaceSomeRightOrLeftCorner_WhenDoubleTall()
        {
            var center = new Point(30, 20);
            _testContext.Init(center);

            _testContext.PutNextRectangle(new Size(8, 10));
            var rect2 = _testContext.PutNextRectangle(new Size(3, 4));


            (rect2.X - center.X).Should().BeOneOf(-7, 4);
            (rect2.Y - center.Y).Should().BeOneOf(-5, 1);
        }

        [Test]
        public void PlaceSomeTopOrBottomCorner_WhenDoubleWide()
        {
            var center = new Point(20, 30);
            _testContext.Init(center);

            _testContext.PutNextRectangle(new Size(10, 8));
            var rect2 = _testContext.PutNextRectangle(new Size(4, 3));

            (rect2.X - center.X).Should().BeOneOf(-5, 1);
            (rect2.Y - center.Y).Should().BeOneOf(-7, 4);
        }

        [TearDown]
        public void TearDown()
        {
            _testContext.DisplayQuality();
            if (TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Failed))
            {
                _testContext.DumpResults();
            }
        }

    }
}