using Azure.Data.Tables;
using TrilhaNetAzureDesafio.Models;

namespace TrilhaNetAzureDesafio.Extensions
{
    public static class TableClientExtensions
    {
        public static void UpsertEntity(this TableClient tableClient, FuncionarioLog funcionarioLog)
        {
            var entity = new TableEntity(funcionarioLog.PartitionKey, funcionarioLog.RowKey)
            {
                {"Departamento", funcionarioLog.Departamento},
                {"TipoAcao", funcionarioLog.TipoAcao},
                {"FuncionarioId", funcionarioLog.FuncionarioId},
                {"Nome", funcionarioLog.Nome}
            };

            tableClient.UpsertEntity(entity);
        }
    }
}
