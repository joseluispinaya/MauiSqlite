using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using MauiSqlite.Mobile.DataAccess;
using MauiSqlite.Mobile.DTOs;
using MauiSqlite.Mobile.Utilidades;
using MauiSqlite.Mobile.Modelos;
using System.Collections.ObjectModel;

namespace MauiSqlite.Mobile.ViewModels
{
    public partial class AddAfiliadoViewModel : ObservableObject, IQueryAttributable
    {
        private readonly EAfiliadoDbContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<EGestionDTO> listaGestion = new ObservableCollection<EGestionDTO>();

        [ObservableProperty]
        private EGestionDTO eGestionDto = new EGestionDTO();

        [ObservableProperty]
        private EAfiliadoDTO eAfiliadoDto = new EAfiliadoDTO();

        [ObservableProperty]
        private string? tituloPagina;

        private int IdAfiliado;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        public AddAfiliadoViewModel(EAfiliadoDbContext context)
        {
            _dbContext = context;
            //CargarGes();
            EAfiliadoDto.Estado = true;
        }
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            IdAfiliado = id;

            if (IdAfiliado == 0)
            {
                TituloPagina = "Nuevo Afiliado";
            }
            else
            {
                TituloPagina = "Editar Afiliado";
                LoadingEsVisible = true;

                await Task.Run(async () =>
                {
                    //var encontrado = await _dbContext.EAfiliados.FirstAsync(e => e.IdAfiliado == IdAfiliado);
                    var encontrado = await _dbContext.EAfiliados
                    .Include(e => e.EGestion)
                    .FirstAsync(e => e.IdAfiliado == IdAfiliado);

                    EAfiliadoDto.IdAfiliado = encontrado.IdAfiliado;
                    EAfiliadoDto.NroCI = encontrado.NroCI;
                    EAfiliadoDto.Nombres = encontrado.Nombres;
                    EAfiliadoDto.Apellidos = encontrado.Apellidos;
                    EAfiliadoDto.Direccion = encontrado.Direccion;
                    EAfiliadoDto.Celular = encontrado.Celular;
                    EAfiliadoDto.Estado = encontrado.Estado;
                    EAfiliadoDto.EGestionId = encontrado.EGestionId;
                    EAfiliadoDto.EGestionDescripcion = encontrado.EGestion?.Descripcion;
                    //lista de gestion

                    MainThread.BeginInvokeOnMainThread(() => { LoadingEsVisible = false; });
                });
            }
            CargarGes();
            //cargo gestiones
        }

        [RelayCommand]
        private async Task Guardar()
        {
            LoadingEsVisible = true;
            EAfiliadoMensaje mensaje = new EAfiliadoMensaje();

            await Task.Run(async () =>
            {
                if (IdAfiliado == 0)
                {
                    var tbEAfiliado = new EAfiliado
                    {
                        //Idasoci = EAfiliadoDto.Idasoci,
                        NroCI = EAfiliadoDto.NroCI,
                        Nombres = EAfiliadoDto.Nombres,
                        Apellidos = EAfiliadoDto.Apellidos,
                        Direccion = EAfiliadoDto.Direccion,
                        Celular = EAfiliadoDto.Celular,
                        Estado = EAfiliadoDto.Estado,
                        EGestionId = EGestionDto.Idges,
                    };

                    _dbContext.EAfiliados.Add(tbEAfiliado);
                    await _dbContext.SaveChangesAsync();

                    EAfiliadoDto.IdAfiliado = tbEAfiliado.IdAfiliado;
                    mensaje = new EAfiliadoMensaje()
                    {
                        EsCrear = true,
                        EAfiliadoDto = EAfiliadoDto
                    };
                }
                else
                {
                    //var encontrado = await _dbContext.EAfiliados.FirstAsync(e => e.IdAfiliado == IdAfiliado);

                    var encontrado = await _dbContext.EAfiliados
                    .Include(e => e.EGestion)
                    .FirstAsync(e => e.IdAfiliado == IdAfiliado);

                    //encontrado.Idasoci = EAfiliadoDto.Idasoci;
                    encontrado.NroCI = EAfiliadoDto.NroCI;
                    encontrado.Nombres = EAfiliadoDto.Nombres;
                    encontrado.Apellidos = EAfiliadoDto.Apellidos;
                    encontrado.Direccion = EAfiliadoDto.Direccion;
                    encontrado.Celular = EAfiliadoDto.Celular;
                    encontrado.Estado = EAfiliadoDto.Estado;
                    encontrado.EGestionId = EGestionDto.Idges;

                    await _dbContext.SaveChangesAsync();

                    mensaje = new EAfiliadoMensaje()
                    {
                        EsCrear = false,
                        EAfiliadoDto = EAfiliadoDto
                    };

                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    LoadingEsVisible = false;
                    WeakReferenceMessenger.Default.Send(new EAfiliadoMensajeria(mensaje));
                    await Shell.Current.Navigation.PopAsync();
                });
            });
        }


        public async void CargarGes()
        {
            var lista = await _dbContext.EGestiones.ToListAsync();
            //listaGestion
            if (lista.Any())
            {
                foreach (var item in lista)
                {
                    ListaGestion.Add(new EGestionDTO
                    {
                        Idges = item.Idges,
                        Descripcion = item.Descripcion,
                        Estado = item.Estado,
                    });
                }
            }

            if (!string.IsNullOrEmpty(EAfiliadoDto.EGestionDescripcion))
            {
                EGestionDto = ListaGestion.FirstOrDefault(g => g.Descripcion == EAfiliadoDto.EGestionDescripcion)!;
            }
        }
    }
}
