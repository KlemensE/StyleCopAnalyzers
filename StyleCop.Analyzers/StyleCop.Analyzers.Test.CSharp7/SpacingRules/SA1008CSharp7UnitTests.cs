﻿// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.CSharp7.SpacingRules
{
    using System.Threading;
    using System.Threading.Tasks;
    using StyleCop.Analyzers.Test.SpacingRules;
    using TestHelper;
    using Xunit;

    using static StyleCop.Analyzers.SpacingRules.SA1008OpeningParenthesisMustBeSpacedCorrectly;

    public class SA1008CSharp7UnitTests : SA1008UnitTests
    {
        /// <summary>
        /// Verifies that spacing around tuple type casts is handled properly.
        /// </summary>
        /// <remarks>
        /// <para>Tuple type casts must be parenthesized, so there are only a limited number of special cases that need
        /// to be added after the ones in <see cref="SA1008UnitTests.TestTypeCastSpacingAsync"/>.</para>
        /// </remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestTupleTypeCastSpacingAsync()
        {
            var testCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod()
        {
            var test1 = ( ( int, int))(3, 3);
            var test2 = ( (int, int))(3, 3);
            var test3 = (( int, int))(3, 3);
        }
    }
}
";

            var fixedCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod()
        {
            var test1 = ((int, int))(3, 3);
            var test2 = ((int, int))(3, 3);
            var test3 = ((int, int))(3, 3);
        }
    }
}
";

            DiagnosticResult[] expectedDiagnostic =
            {
                // test1
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(7, 25),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(7, 27),

                // test2
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(8, 25),

                // test3
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(9, 26),
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expectedDiagnostic, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpFixAsync(testCode, fixedCode).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that spacing around tuple types in parameters is handled properly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        /// <seealso cref="SA1008UnitTests.TestParameterListsAsync"/>
        [Fact]
        public async Task TestTupleParameterTypeAsync()
        {
            var testCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public int TestMethod1( ( int, int) arg) => 0;
        public int TestMethod2( (int, int) arg) => 0;
        public int TestMethod3(( int, int) arg) => 0;

        public int TestMethod4((int, int) arg1,( int, int) arg2) => 0;
        public int TestMethod5((int, int) arg1,(int, int) arg2) => 0;
        public int TestMethod6((int, int) arg1, ( int, int) arg2) => 0;
    }
}
";

            var fixedCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public int TestMethod1((int, int) arg) => 0;
        public int TestMethod2((int, int) arg) => 0;
        public int TestMethod3((int, int) arg) => 0;

        public int TestMethod4((int, int) arg1, (int, int) arg2) => 0;
        public int TestMethod5((int, int) arg1, (int, int) arg2) => 0;
        public int TestMethod6((int, int) arg1, (int, int) arg2) => 0;
    }
}
";

            DiagnosticResult[] expectedDiagnostic =
            {
                // TestMethod1
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(5, 31),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(5, 33),

                // TestMethod2
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(6, 31),

                // TestMethod3
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(7, 32),

                // TestMethod4
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(9, 48),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(9, 48),

                // TestMethod5
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(10, 48),

                // TestMethod6
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(11, 49),
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expectedDiagnostic, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpFixAsync(testCode, fixedCode, numberOfFixAllIterations: 2).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(2472, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/2472")]
        public async Task TestTupleArrayParameterTypeAsync()
        {
            var testCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public int TestMethod1( ( int, int)[] arg) => 0;
        public int TestMethod2( (int, int)[] arg) => 0;
        public int TestMethod3(( int, int)[] arg) => 0;

        public int TestMethod4((int, int) arg1, params( int, int)[] arg2) => 0;
        public int TestMethod5((int, int) arg1, params(int, int)[] arg2) => 0;
        public int TestMethod6((int, int) arg1, params ( int, int)[] arg2) => 0;
    }
}
";

            var fixedCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public int TestMethod1((int, int)[] arg) => 0;
        public int TestMethod2((int, int)[] arg) => 0;
        public int TestMethod3((int, int)[] arg) => 0;

        public int TestMethod4((int, int) arg1, params (int, int)[] arg2) => 0;
        public int TestMethod5((int, int) arg1, params (int, int)[] arg2) => 0;
        public int TestMethod6((int, int) arg1, params (int, int)[] arg2) => 0;
    }
}
";

            DiagnosticResult[] expectedDiagnostic =
            {
                // TestMethod1
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(5, 31),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(5, 33),

                // TestMethod2
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(6, 31),

                // TestMethod3
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(7, 32),

                // TestMethod4
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(9, 55),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(9, 55),

                // TestMethod5
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(10, 55),

                // TestMethod6
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(11, 56),
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expectedDiagnostic, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpFixAsync(testCode, fixedCode, numberOfFixAllIterations: 2).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(2472, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/2472")]
        public async Task TestTupleOutParametersAsync()
        {
            var testCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public int TestMethod1(out( int, int)[] arg) => throw null;
        public int TestMethod2(out(int, int)[] arg) => throw null;
        public int TestMethod3(out ( int, int)[] arg) => throw null;
    }
}
";

            var fixedCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public int TestMethod1(out (int, int)[] arg) => throw null;
        public int TestMethod2(out (int, int)[] arg) => throw null;
        public int TestMethod3(out (int, int)[] arg) => throw null;
    }
}
";

            DiagnosticResult[] expectedDiagnostic =
            {
                // TestMethod1
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(5, 35),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(5, 35),

                // TestMethod2
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(6, 35),

                // TestMethod3
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(7, 36),
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expectedDiagnostic, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpFixAsync(testCode, fixedCode, numberOfFixAllIterations: 2).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that spacing around tuple return types is handled properly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestTupleReturnTypeAsync()
        {
            var testCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public( int, int) TestMethod1() => default( ( int, int));
        public(int, int) TestMethod2() => default( (int, int));
        public ( int, int) TestMethod3() => default(( int, int));

        public ( ( int, int), int) TestMethod4() => default(( ( int, int), int));
        public ( (int, int), int) TestMethod5() => default(( (int, int), int));
        public (( int, int), int) TestMethod6() => default((( int, int), int));

        public (int,( int, int)) TestMethod7() => default((int,( int, int)));
        public (int,(int, int)) TestMethod8() => default((int,(int, int)));
        public (int, ( int, int)) TestMethod9() => default((int, ( int, int)));

        public( int x, int y) TestMethod10() => default( ( int x, int y));
        public(int x, int y) TestMethod11() => default( (int x, int y));
        public ( int x, int y) TestMethod12() => default(( int x, int y));

        public ( ( int x, int y), int z) TestMethod13() => default(( ( int x, int y), int z));
        public ( (int x, int y), int z) TestMethod14() => default(( (int x, int y), int z));
        public (( int x, int y), int z) TestMethod15() => default((( int x, int y), int z));

        public (int x,( int y, int z)) TestMethod16() => default((int x,( int y, int z)));
        public (int x,(int y, int z)) TestMethod17() => default((int x,(int y, int z)));
        public (int x, ( int y, int z)) TestMethod18() => default((int x, ( int y, int z)));
    }
}
";

            var fixedCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public (int, int) TestMethod1() => default((int, int));
        public (int, int) TestMethod2() => default((int, int));
        public (int, int) TestMethod3() => default((int, int));

        public ((int, int), int) TestMethod4() => default(((int, int), int));
        public ((int, int), int) TestMethod5() => default(((int, int), int));
        public ((int, int), int) TestMethod6() => default(((int, int), int));

        public (int, (int, int)) TestMethod7() => default((int, (int, int)));
        public (int, (int, int)) TestMethod8() => default((int, (int, int)));
        public (int, (int, int)) TestMethod9() => default((int, (int, int)));

        public (int x, int y) TestMethod10() => default((int x, int y));
        public (int x, int y) TestMethod11() => default((int x, int y));
        public (int x, int y) TestMethod12() => default((int x, int y));

        public ((int x, int y), int z) TestMethod13() => default(((int x, int y), int z));
        public ((int x, int y), int z) TestMethod14() => default(((int x, int y), int z));
        public ((int x, int y), int z) TestMethod15() => default(((int x, int y), int z));

        public (int x, (int y, int z)) TestMethod16() => default((int x, (int y, int z)));
        public (int x, (int y, int z)) TestMethod17() => default((int x, (int y, int z)));
        public (int x, (int y, int z)) TestMethod18() => default((int x, (int y, int z)));
    }
}
";

            DiagnosticResult[] expectedDiagnostic =
            {
                // TestMethod1, TestMethod2, TestMethod3
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(5, 15),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(5, 15),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(5, 51),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(5, 53),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(6, 15),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(6, 50),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(7, 16),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(7, 53),

                // TestMethod4, TestMethod5, TestMethod6
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(9, 16),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(9, 18),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(9, 61),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(9, 63),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(10, 16),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(10, 60),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(11, 17),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(11, 61),

                // TestMethod7, TestMethod8, TestMethod9
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(13, 21),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(13, 21),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(13, 64),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(13, 64),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(14, 21),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(14, 63),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(15, 22),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(15, 66),

                // TestMethod10, TestMethod11, TestMethod12
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(17, 15),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(17, 15),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(17, 56),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(17, 58),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(18, 15),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(18, 55),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(19, 16),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(19, 58),

                // TestMethod13, TestMethod14, TestMethod15
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(21, 16),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(21, 18),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(21, 68),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(21, 70),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(22, 16),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(22, 67),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(23, 17),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(23, 68),

                // TestMethod16, TestMethod17, TestMethod18
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(25, 23),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(25, 23),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(25, 73),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(25, 73),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(26, 23),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(26, 72),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(27, 24),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(27, 75),
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expectedDiagnostic, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpFixAsync(testCode, fixedCode, numberOfFixAllIterations: 2).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(2472, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/2472")]
        public async Task TestNullableTupleReturnTypeAsync()
        {
            var testCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public( int, int)? TestMethod1() => default( ( int, int)?);
        public(int, int)? TestMethod2() => default( (int, int)?);
        public ( int, int)? TestMethod3() => default(( int, int)?);
    }
}
";

            var fixedCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public (int, int)? TestMethod1() => default((int, int)?);
        public (int, int)? TestMethod2() => default((int, int)?);
        public (int, int)? TestMethod3() => default((int, int)?);
    }
}
";

            DiagnosticResult[] expectedDiagnostic =
            {
                // TestMethod1, TestMethod2, TestMethod3
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(5, 15),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(5, 15),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(5, 52),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(5, 54),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(6, 15),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(6, 51),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(7, 16),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(7, 54),
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expectedDiagnostic, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpFixAsync(testCode, fixedCode, numberOfFixAllIterations: 2).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that spacing for tuple expressions is handled properly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        /// <seealso cref="SA1000CSharp7UnitTests.TestReturnTupleExpressionsAsync"/>
        [Fact]
        public async Task TestTupleExpressionsAsync()
        {
            var testCode = @"using System.Collections.Generic;

namespace TestNamespace
{
    public class TestClass
    {
        private static Dictionary<(int, int), (int, int)> dictionary;

        public void TestMethod()
        {
            // Top level
            var v1 =( 1, 2);
            var v2 =(1, 2);
            var v3 = ( 1, 2);

            // Nested first element
            var v4 = ( ( 1, 3), 2);
            var v5 = ( (1, 3), 2);
            var v6 = (( 1, 3), 2);

            // Nested after first element
            var v7 = (1,( 2, 3));
            var v8 = (1,(2, 3));
            var v9 = (1, ( 2, 3));

            // Top level, name inside
            var v10 =( x: 1, 2);
            var v11 =(x: 1, 2);
            var v12 = ( x: 1, 2);

            // Nested first element, name inside
            var v13 = ( ( x: 1, 3), 2);
            var v14 = ( (x: 1, 3), 2);
            var v15 = (( x: 1, 3), 2);

            // Nested after first element, name inside
            var v16 = (1,( x: 2, 3));
            var v17 = (1,(x: 2, 3));
            var v18 = (1, ( x: 2, 3));

            // Nested first element, name outside
            var v19 = (x:( 1, 3), 2);
            var v20 = (x:(1, 3), 2);
            var v21 = (x: ( 1, 3), 2);

            // Nested after first element, name outside
            var v22 = (1, x:( 2, 3));
            var v23 = (1, x:(2, 3));
            var v24 = (1, x: ( 2, 3));

            // Indexer
            var v25 = dictionary[ ( 1, 2)];
            var v26 = dictionary[ (1, 2)];
            var v27 = dictionary[( 1, 2)];
            var v28 = dictionary[key:( 1, 2)];
            var v29 = dictionary[key:(1, 2)];
            var v30 = dictionary[key: ( 1, 2)];

            // First argument
            dictionary.Add( ( 1, 2), (1, 2));
            dictionary.Add( (1, 2), (1, 2));
            dictionary.Add(( 1, 2), (1, 2));
            dictionary.Add(key:( 1, 2), value: (1, 2));
            dictionary.Add(key:(1, 2), value: (1, 2));
            dictionary.Add(key: ( 1, 2), value: (1, 2));

            // Second argument
            dictionary.Add((1, 2),( 1, 2));
            dictionary.Add((1, 2),(1, 2));
            dictionary.Add((1, 2), ( 1, 2));
            dictionary.Add(key: (1, 2), value:( 1, 2));
            dictionary.Add(key: (1, 2), value:(1, 2));
            dictionary.Add(key: (1, 2), value: ( 1, 2));

            // Returns (leading spaces after keyword checked in SA1000, not SA1008)
            (int, int) LocalFunction1() { return( 1, 2); }
            (int, int) LocalFunction2() { return(1, 2); }
            (int, int) LocalFunction3() { return ( 1, 2); }
            (int, int) LocalFunction4() =>( 1, 2);
            (int, int) LocalFunction5() =>(1, 2);
            (int, int) LocalFunction6() => ( 1, 2);
        }
    }
}
";

            var fixedCode = @"using System.Collections.Generic;

namespace TestNamespace
{
    public class TestClass
    {
        private static Dictionary<(int, int), (int, int)> dictionary;

        public void TestMethod()
        {
            // Top level
            var v1 = (1, 2);
            var v2 = (1, 2);
            var v3 = (1, 2);

            // Nested first element
            var v4 = ((1, 3), 2);
            var v5 = ((1, 3), 2);
            var v6 = ((1, 3), 2);

            // Nested after first element
            var v7 = (1, (2, 3));
            var v8 = (1, (2, 3));
            var v9 = (1, (2, 3));

            // Top level, name inside
            var v10 = (x: 1, 2);
            var v11 = (x: 1, 2);
            var v12 = (x: 1, 2);

            // Nested first element, name inside
            var v13 = ((x: 1, 3), 2);
            var v14 = ((x: 1, 3), 2);
            var v15 = ((x: 1, 3), 2);

            // Nested after first element, name inside
            var v16 = (1, (x: 2, 3));
            var v17 = (1, (x: 2, 3));
            var v18 = (1, (x: 2, 3));

            // Nested first element, name outside
            var v19 = (x: (1, 3), 2);
            var v20 = (x: (1, 3), 2);
            var v21 = (x: (1, 3), 2);

            // Nested after first element, name outside
            var v22 = (1, x: (2, 3));
            var v23 = (1, x: (2, 3));
            var v24 = (1, x: (2, 3));

            // Indexer
            var v25 = dictionary[(1, 2)];
            var v26 = dictionary[(1, 2)];
            var v27 = dictionary[(1, 2)];
            var v28 = dictionary[key: (1, 2)];
            var v29 = dictionary[key: (1, 2)];
            var v30 = dictionary[key: (1, 2)];

            // First argument
            dictionary.Add((1, 2), (1, 2));
            dictionary.Add((1, 2), (1, 2));
            dictionary.Add((1, 2), (1, 2));
            dictionary.Add(key: (1, 2), value: (1, 2));
            dictionary.Add(key: (1, 2), value: (1, 2));
            dictionary.Add(key: (1, 2), value: (1, 2));

            // Second argument
            dictionary.Add((1, 2), (1, 2));
            dictionary.Add((1, 2), (1, 2));
            dictionary.Add((1, 2), (1, 2));
            dictionary.Add(key: (1, 2), value: (1, 2));
            dictionary.Add(key: (1, 2), value: (1, 2));
            dictionary.Add(key: (1, 2), value: (1, 2));

            // Returns (leading spaces after keyword checked in SA1000, not SA1008)
            (int, int) LocalFunction1() { return(1, 2); }
            (int, int) LocalFunction2() { return(1, 2); }
            (int, int) LocalFunction3() { return (1, 2); }
            (int, int) LocalFunction4() => (1, 2);
            (int, int) LocalFunction5() => (1, 2);
            (int, int) LocalFunction6() => (1, 2);
        }
    }
}
";

            DiagnosticResult[] expectedDiagnostics =
            {
                // v1, v2, v3
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(12, 21),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(12, 21),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(13, 21),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(14, 22),

                // v4, v5, v6
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(17, 22),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(17, 24),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(18, 22),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(19, 23),

                // v7, v8, v9
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(22, 25),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(22, 25),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(23, 25),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(24, 26),

                // v10, v11, v12
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(27, 22),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(27, 22),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(28, 22),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(29, 23),

                // v13, v14, v15
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(32, 23),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(32, 25),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(33, 23),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(34, 24),

                // v16, v17, v18
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(37, 26),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(37, 26),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(38, 26),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(39, 27),

                // v19, v20, v21
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(42, 26),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(42, 26),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(43, 26),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(44, 27),

                // v22, v23, v24
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(47, 29),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(47, 29),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(48, 29),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(49, 30),

                // v25, v26, v27
                this.CSharpDiagnostic(DescriptorNotPreceded).WithLocation(52, 35),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(52, 35),
                this.CSharpDiagnostic(DescriptorNotPreceded).WithLocation(53, 35),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(54, 34),

                // v28, v29, v30
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(55, 38),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(55, 38),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(56, 38),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(57, 39),

                // First argument
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(60, 27),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(60, 29),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(61, 27),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(62, 28),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(63, 32),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(63, 32),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(64, 32),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(65, 33),

                // Second argument
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(68, 35),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(68, 35),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(69, 35),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(70, 36),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(71, 47),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(71, 47),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(72, 47),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(73, 48),

                // Returns
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(76, 49),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(78, 50),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(79, 43),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(79, 43),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(80, 43),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(81, 44),
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expectedDiagnostics, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpFixAsync(testCode, fixedCode, numberOfFixAllIterations: 2).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that spacing for tuple types used as generic arguments is handled properly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestTupleTypesAsGenericArgumentsAsync()
        {
            var testCode = @"using System;

namespace TestNamespace
{
    public class TestClass
    {
        static Func< ( int, int), (int, int)> Function1;
        static Func< (int, int), (int, int)> Function2;
        static Func<( int, int), (int, int)> Function3;

        static Func<(int, int),( int, int)> Function4;
        static Func<(int, int),(int, int)> Function5;
        static Func<(int, int), ( int, int)> Function6;
    }
}
";

            var fixedCode = @"using System;

namespace TestNamespace
{
    public class TestClass
    {
        static Func<(int, int), (int, int)> Function1;
        static Func<(int, int), (int, int)> Function2;
        static Func<(int, int), (int, int)> Function3;

        static Func<(int, int), (int, int)> Function4;
        static Func<(int, int), (int, int)> Function5;
        static Func<(int, int), (int, int)> Function6;
    }
}
";

            DiagnosticResult[] expectedDiagnostics =
            {
                this.CSharpDiagnostic(DescriptorNotPreceded).WithLocation(7, 22),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(7, 22),
                this.CSharpDiagnostic(DescriptorNotPreceded).WithLocation(8, 22),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(9, 21),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(11, 32),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(11, 32),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(12, 32),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(13, 33),
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expectedDiagnostics, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpFixAsync(testCode, fixedCode, numberOfFixAllIterations: 2).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that spacing for tuple variable declarations is handled properly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestTupleVariablesAsync()
        {
            var testCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod()
        {
            var( x1, y1) = (1, 2);
            var(x2, y2) = (1, 2);
            var ( x3, y3) = (1, 2);
        }
    }
}
";

            var fixedCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod()
        {
            var (x1, y1) = (1, 2);
            var (x2, y2) = (1, 2);
            var (x3, y3) = (1, 2);
        }
    }
}
";

            DiagnosticResult[] expectedDiagnostics =
            {
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(7, 16),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(7, 16),
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(8, 16),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(9, 17),
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expectedDiagnostics, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpFixAsync(testCode, fixedCode, numberOfFixAllIterations: 2).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that spacing for <c>ref</c> expressions is handled properly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestRefExpressionAsync()
        {
            var testCode = @"namespace TestNamespace
{
    using System.Threading.Tasks;

    public class TestClass
    {
        public void TestMethod()
        {
            int test = 1;
            ref int t = ref( test);
        }
    }
}
";

            var fixedCode = @"namespace TestNamespace
{
    using System.Threading.Tasks;

    public class TestClass
    {
        public void TestMethod()
        {
            int test = 1;
            ref int t = ref (test);
        }
    }
}
";

            DiagnosticResult[] expectedDiagnostics =
            {
                this.CSharpDiagnostic(DescriptorPreceded).WithLocation(10, 28),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(10, 28),
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expectedDiagnostics, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpFixAsync(testCode, fixedCode, numberOfFixAllIterations: 2).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that spacing for <c>new</c> expressions for an array of a tuple type is handled correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        /// <seealso cref="SA1000CSharp7UnitTests.TestNewTupleArrayAsync"/>
        [Fact]
        public async Task TestNewTupleArrayAsync()
        {
            var testCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod()
        {
            var x = new( int, int)[0];
            var y = new(int, int)[0];
            var z = new ( int, int)[0];
        }
    }
}
";

            var fixedCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod()
        {
            var x = new(int, int)[0];
            var y = new(int, int)[0];
            var z = new (int, int)[0];
        }
    }
}
";

            DiagnosticResult[] expectedDiagnostics =
            {
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(7, 24),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(9, 25),
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expectedDiagnostics, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpFixAsync(testCode, fixedCode).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that spacing for <c>foreach</c> expressions using tuple deconstruction is handled properly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        /// <seealso cref="SA1000CSharp7UnitTests.TestForEachVariableStatementAsync"/>
        [Fact]
        public async Task TestForEachVariableStatementAsync()
        {
            var testCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod()
        {
            foreach( var (x, y) in new (int, int)[0]) { }
            foreach(var (x, y) in new (int, int)[0]) { }
            foreach ( var (x, y) in new (int, int)[0]) { }
        }
    }
}
";

            var fixedCode = @"namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod()
        {
            foreach(var (x, y) in new (int, int)[0]) { }
            foreach(var (x, y) in new (int, int)[0]) { }
            foreach (var (x, y) in new (int, int)[0]) { }
        }
    }
}
";

            DiagnosticResult[] expectedDiagnostics =
            {
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(7, 20),
                this.CSharpDiagnostic(DescriptorNotFollowed).WithLocation(9, 21),
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expectedDiagnostics, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpFixAsync(testCode, fixedCode).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(2475, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/2475")]
        public async Task TestSingleLineIfStatementWithTupleExpressionAsync()
        {
            var testCode = @"public class TestClass
{
    public void TestMethod()
    {
        if (true) (1, 2).ToString();
    }
}
";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(2521, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/2521")]
        public async Task TestTupleInLambdaAsync()
        {
            var testCode = @"namespace TestNamespace
{
    using System.Collections.Generic;
    using System.Linq;

    public class TestClass
    {
        public void TestMethod()
        {
            var testList = new List<int>();
            testList.Select((number, i) => (number, i));
        }
    }
}
";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }
    }
}
