using System;
using System.Linq;
using System.Reflection;
using Scellecs.Morpeh.Collections;
using Scellecs.Morpeh.Native;
using UnityEngine;

namespace Scellecs.Morpeh
{
    internal static class QueryHelper
    {
#region Validation

        internal struct RequestedTypeInfo
        {
            public Type type;
            public long typeId;

            public RequestedTypeInfo(Type type) : this()
            {
                this.type = type;
            }

            public RequestedTypeInfo(Type type, long typeId)
            {
                this.type = type;
                this.typeId = typeId;
            }
        }

        internal static RequestedTypeInfo GetRequestedTypeInfo<T>() where T : struct, IComponent
        {
            return new RequestedTypeInfo(typeof(T), TypeIdentifier<T>.info.id);
        }

        private const string TYPE_IDENTIFIER_INFO_FIELD = "info";
        
        internal static CommonTypeIdentifier.TypeInfo GetTypeInfoByType(Type type)
        {
            return (CommonTypeIdentifier.TypeInfo)typeof(TypeIdentifier<>)
                .MakeGenericType(type)
                .GetField(TYPE_IDENTIFIER_INFO_FIELD, BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic)!
                .GetValue(null);
        }

        internal static void ValidateRequest(QueryBuilder queryBuilder, CompiledQuery compiledQuery, params RequestedTypeInfo[] requestedTypeInfosToValidate)
        {
            // we can't validate the query if filter is empty
            if (!compiledQuery.hasFilter)
                return;
            
            var hasProblems = false;
            foreach (var requestedTypeInfo in requestedTypeInfosToValidate)
            {
                if (!compiledQuery.filter.includedTypeIds.data.Contains(requestedTypeInfo.typeId))
                    Debug.LogError(
                        $"You're expecting a component [<b>{requestedTypeInfo.type.Name}</b>] in your query in [<b>{queryBuilder.System.GetType().Name}</b>], but the query is <b>missing</b> this parameter. Please add it to the query first!");

                if (compiledQuery.filter.excludedTypeIds.data.Contains(requestedTypeInfo.typeId))
                    Debug.LogError(
                        $"You're expecting a component [<b>{requestedTypeInfo.type.Name}</b>] in your query in [<b>{queryBuilder.System.GetType().Name}</b>], but the query is <b>deliberately excluded</b> this parameter. Please remove it from the query or from the ForEach lambda!");
            }

            if (hasProblems)
                throw new Exception($"There were problems when configuring a query for [<b>{queryBuilder.System.GetType().Name}</b>]. Please fix those first");
        }

#endregion
    }
}