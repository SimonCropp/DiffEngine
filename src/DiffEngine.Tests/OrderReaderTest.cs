﻿using System;
using System.Linq;
using DiffEngine;
using Xunit;
using Xunit.Abstractions;

public class OrderReaderTest :
    XunitContextBase
{
    [Fact]
    public void ParseEnvironmentVariable()
    {
        var diffTools = OrderReader.ParseEnvironment("VisualStudio,Meld").ToList();
        Assert.Equal(DiffTool.VisualStudio, diffTools[0]);
        Assert.Equal(DiffTool.Meld, diffTools[1]);
    }

    [Fact]
    public void BadEnvironmentVariable()
    {
        var exception = Assert.Throws<Exception>(() => OrderReader.ParseEnvironment("Foo").ToList());
        Assert.Equal("Unable to parse tool from `DiffEngine.ToolOrder` environment variable: Foo", exception.Message);
    }

    public OrderReaderTest(ITestOutputHelper output) :
        base(output)
    {
    }
}