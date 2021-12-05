// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMultitenancy.Multitenancy
{
    public class PostgresTenantDbConnectionInterceptor : DbConnectionInterceptor
    {
        public override void ConnectionOpened(DbConnection connection, ConnectionEndEventData eventData)
        {
            if(TenantExecutionContext.Tenant != null)
            {
                using (var cmd = connection.CreateCommand())
                {
                    PrepareCommand(cmd, TenantExecutionContext.Tenant);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public override async Task ConnectionOpenedAsync(DbConnection connection, ConnectionEndEventData eventData, CancellationToken cancellationToken = default)
        {
            if (TenantExecutionContext.Tenant != null)
            {
                using (var cmd = connection.CreateCommand())
                {
                    PrepareCommand(cmd, TenantExecutionContext.Tenant);

                    await cmd.ExecuteNonQueryAsync(cancellationToken);
                }
            }
        }

        private void PrepareCommand(DbCommand cmd, Tenant tenant)
        {
            cmd.CommandText = "SET app.current_tenant = @tenant_id";

            DbParameter pTenantName = cmd.CreateParameter();

            pTenantName.ParameterName = "@tenant_id";
            pTenantName.Value = tenant.Name;

            cmd.Parameters.Add(pTenantName);
        }
    }
}
