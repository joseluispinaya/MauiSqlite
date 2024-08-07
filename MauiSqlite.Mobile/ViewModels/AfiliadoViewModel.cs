using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using MauiSqlite.Mobile.DataAccess;
using MauiSqlite.Mobile.DTOs;
using MauiSqlite.Mobile.Utilidades;
using MauiSqlite.Mobile.Modelos;
using System.Collections.ObjectModel;
using MauiSqlite.Mobile.Repositories;
using MauiSqlite.Mobile.Views;
using MauiSqlite.Mobile.Models;

namespace MauiSqlite.Mobile.ViewModels
{
    public partial class AfiliadoViewModel : ObservableObject
    {
        private readonly IRepository _repository;
        private readonly EAfiliadoDbContext _dbContext;
        [ObservableProperty]
        private ObservableCollection<EAfiliadoDTO> listaAfiliados = new ObservableCollection<EAfiliadoDTO>();

        public AfiliadoViewModel(EAfiliadoDbContext context, IRepository repository)
        {
            _dbContext = context;
            _repository = repository;
            //LoadGestiones();
            MainThread.BeginInvokeOnMainThread(new Action(async () => await Obtener()));

            WeakReferenceMessenger.Default.Register<EAfiliadoMensajeria>(this, (r, m) =>
            {
                EAfiliadoMensajeRecibido(m.Value);
            });
        }

        private async void LoadGestiones()
        {
            string url = "https://umapedis-001-site1.ftempurl.com/";
            var responseHttp = await _repository.Get<List<EGestionA>>(url, "api/gestiones/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await Shell.Current.DisplayAlert("Error", message, "Ok");
                return;
            }

            List<EGestionA> eGestionsa = new List<EGestionA>();
            eGestionsa = responseHttp.Response!;
            //var listagest = responseHttp.Response!;

            foreach (var gestion in eGestionsa)
            {
                var tbEGestion = new EGestion
                {
                    Idges = gestion.Idges,
                    Descripcion = gestion.Descripcion,
                    Estado = true,
                };
                _dbContext.EGestiones.Add(tbEGestion);
            }
            await _dbContext.SaveChangesAsync();
        }


        public async Task Obtener()
        {
            var lista = await _dbContext.EAfiliados.ToListAsync();
            if (lista.Any())
            {
                foreach (var item in lista)
                {
                    ListaAfiliados.Add(new EAfiliadoDTO
                    {
                        IdAfiliado = item.IdAfiliado,
                        NroCI = item.NroCI,
                        Nombres = item.Nombres,
                        Apellidos = item.Apellidos,
                        Direccion = item.Direccion,
                        Celular = item.Celular,
                        Estado = item.Estado,
                        EGestionId = item.EGestionId,

                        //EAfiliadoDto.EGestionId = encontrado.EGestionId;
                    });
                }
            }
        }

        private void EAfiliadoMensajeRecibido(EAfiliadoMensaje eafiliadoMensaje)
        {
            var eafiliadoDto = eafiliadoMensaje.EAfiliadoDto!;

            if (eafiliadoMensaje.EsCrear)
            {
                ListaAfiliados.Add(eafiliadoDto);
            }
            else
            {
                var encontrado = ListaAfiliados
                    .First(e => e.IdAfiliado == eafiliadoDto.IdAfiliado);

                encontrado.NroCI = eafiliadoDto.NroCI;
                encontrado.Nombres = eafiliadoDto.Nombres;
                encontrado.Apellidos = eafiliadoDto.Apellidos;
                encontrado.Direccion = eafiliadoDto.Direccion;
                encontrado.Celular = eafiliadoDto.Celular;
                encontrado.Estado = eafiliadoDto.Estado;
                encontrado.EGestionId = eafiliadoDto.EGestionId;
            }
        }

        [RelayCommand]
        private async Task Crear()
        {
            var uri = $"{nameof(AddAfiliadoView)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Editar(EAfiliadoDTO eAfiliadoDto)
        {
            var uri = $"{nameof(AddAfiliadoView)}?id={eAfiliadoDto.IdAfiliado}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Eliminar(EAfiliadoDTO eAfiliadoDto)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar el Afiliado?", "Si", "No");

            if (answer)
            {
                var encontrado = await _dbContext.EAfiliados
                    .FirstAsync(e => e.IdAfiliado == eAfiliadoDto.IdAfiliado);

                _dbContext.EAfiliados.Remove(encontrado);
                await _dbContext.SaveChangesAsync();
                ListaAfiliados.Remove(eAfiliadoDto);

            }

        }
    }
}
