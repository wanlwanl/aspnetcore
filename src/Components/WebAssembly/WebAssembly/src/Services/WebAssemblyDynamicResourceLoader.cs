// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.JSInterop.WebAssembly;

namespace Microsoft.AspNetCore.Components.WebAssembly.Services
{
    public class WebAssemblyDynamicResourceLoader
    {
        internal const string GetDynamicAssemblies = "window.Blazor._internal.getDynamicAssemblies";
        internal const string ReadDynamicAssemblies = "window.Blazor._internal.readDynamicAssemblies";

        private readonly WebAssemblyJSRuntime _jsRuntime;

        internal WebAssemblyDynamicResourceLoader(WebAssemblyJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public virtual async ValueTask LoadDynamicAssemblies(IEnumerable<string> assembliesToLoad)
        {
            var count = (int)await _jsRuntime.InvokeUnmarshalled<string[], object, object, Task<object>>(
                GetDynamicAssemblies,
                assembliesToLoad.ToArray(),
                null,
                null);

            if (count == 0)
            {
                return;
            }

            var assemblies = _jsRuntime.InvokeUnmarshalled<object, object, object, object[]>(
                ReadDynamicAssemblies,
                null,
                null,
                null);

            foreach (byte[] assembly in assemblies)
            {
                Assembly.Load(assembly);
            }
        }
    }
}
