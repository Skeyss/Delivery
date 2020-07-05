using Delivery.Core;
using Delivery.Models;
using Delivery.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services.Manager
{
    public class AgrupacionManager
    {
		IAgrupacion agrupacionService;

		public AgrupacionManager()
		{
			agrupacionService = new AgrupacionService();
		}

		public Task<EstadoDeConsulta> GetTasksAsync(bool forceRefresh)
		{
			//IEnumerable<Agrupacion>
			return   agrupacionService.GetAgrupacionesAsync(forceRefresh);
		}

		//public Task SaveTaskAsync(TodoItem item, bool isNewItem = false)
		//{
		//	return restService.SaveTodoItemAsync(item, isNewItem);
		//}

		//public Task DeleteTaskAsync(TodoItem item)
		//{
		//	return restService.DeleteTodoItemAsync(item.ID);
		//}

	}
}
