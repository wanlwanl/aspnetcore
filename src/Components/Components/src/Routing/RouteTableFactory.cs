// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Internal;

namespace Microsoft.AspNetCore.Components
{
    /// <summary>
    /// Resolves components for an application.
    /// </summary>
    internal static class RouteTableFactory
    {
        public static RouteTable Create(IEnumerable<Assembly> assemblies)
        {
            var routeTable = new RouteTable();
            routeTable.AddRoutesFromAssemblies(assemblies);
            return routeTable;
        }

        internal static RouteTable Create(IEnumerable<Type> componentTypes)
        {
            var routeTable = new RouteTable();
            routeTable.AddRoutesFromComponent(componentTypes);
            return routeTable;
        }

        internal static RouteTable Create(Dictionary<Type, string[]> templatesByHandler)
        {
            var routeTable = new RouteTable();
            routeTable.AddRoutesFromTemplates(templatesByHandler);
            return routeTable;
        }
    }
}
