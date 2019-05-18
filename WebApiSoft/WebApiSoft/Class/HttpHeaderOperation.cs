using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApiSoft.Class
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpHeaderOperation : IOperationFilter
    {
        /// <summary>
        /// 实现接口
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            context.ApiDescription.TryGetMethodInfo(out var info);

            try
            {
                var isAuthorized = info.GetCustomAttribute(typeof(AuthorizeAttribute));
                if (isAuthorized != null)
                {
                    operation.Parameters.Add(new NonBodyParameter()
                    {
                        Name = "Authorization", //添加Authorization头部参数
                        In = "header",
                        Type = "string",
                        Description = "access_token",
                        Required = true
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}