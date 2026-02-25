using System;
using UnityEngine;

namespace Scellecs.Morpeh
{
    internal static class QueryHelper
    {
        #region Validation

        internal struct RequestedTypeInfo
        {
            public Type type;
            public int typeId;

            public RequestedTypeInfo(Type type) : this()
            {
                this.type = type;
            }

            public RequestedTypeInfo(Type type, int typeId)
            {
                this.type = type;
                this.typeId = typeId;
            }
        }

        internal static RequestedTypeInfo GetRequestedTypeInfo<T>() where T : struct, IComponent
        {
            return new RequestedTypeInfo(typeof(T), ComponentId<T>.info.id);
        }

        internal static void ValidateRequest(QueryBuilder queryBuilder, CompiledQuery compiledQuery, params RequestedTypeInfo[] requestedTypeInfosToValidate)
        {
            // we can't validate the query if filter is empty
            if (!compiledQuery.hasFilter)
                return;

            var hasProblems = false;
            foreach (var requestedTypeInfo in requestedTypeInfosToValidate)
            {
                if (!compiledQuery.filter.includedTypeIdsLookup.IsSet(requestedTypeInfo.typeId))
                {
                    Debug.LogError(
                        $"You're expecting a component [<b>{requestedTypeInfo.type.Name}</b>] in your query in [<b>{queryBuilder.System.GetType().Name}</b>], but the query is <b>missing</b> this parameter. Please add it to the query first!");
                    hasProblems = true;
                }

                if (compiledQuery.filter.excludedTypeIdsLookup.IsSet(requestedTypeInfo.typeId))
                {
                    Debug.LogError(
                        $"You're expecting a component [<b>{requestedTypeInfo.type.Name}</b>] in your query in [<b>{queryBuilder.System.GetType().Name}</b>], but the query is <b>deliberately excluded</b> this parameter. Please remove it from the query or from the ForEach lambda!");
                    hasProblems = true;
                }
            }

            if (hasProblems)
                throw new Exception($"There were problems when configuring a query for [<b>{queryBuilder.System.GetType().Name}</b>]. Please fix those first");
        }

        #endregion
    }
}