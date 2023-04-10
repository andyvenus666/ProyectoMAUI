//using Android.OS;
using ClienteMAUI.ConexionDatos;
using ClienteMAUI.Models;
using System.Diagnostics;

namespace ClienteMAUI.Pages;
[QueryProperty(nameof(plato), "Plato")]
public partial class GestionPlatosPage : ContentPage
{
    private readonly IRestConexionDatos conexionDatos;
	private Plato _plato;
	private bool _esNuevo;//para nuevo registro
	public Plato plato
	{
		get => _plato;
        set
        {
				_esNuevo = esNuevo(value);
				_plato = value;
				OnPropertyChanged();//obligatorio para ctualizar el siguiente plato
			
		}
    }
	public GestionPlatosPage(IRestConexionDatos conexionDatos)
    {

        InitializeComponent();
		this.conexionDatos = conexionDatos;
		BindingContext = this; //para enlazar los datos no olvidar es obligatorio
	}
	bool esNuevo(Plato plato)
	{
		if (plato.Id == 0)
			return true;
		return false;
	}
	async void OnCancelarClic(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
	async void OnGuardarClic(object sender, EventArgs e)
	{
		if (_esNuevo)
		{
			Debug.WriteLine("[REGISTRO] agregando nuevo plato");
			await conexionDatos.AddPlatoAsync(_plato);	
		}
		else
		{
            Debug.WriteLine("[REGISTRO] Cambiando el registro");
            await conexionDatos.UpdatePlatoAsync(_plato);
        }
        await Shell.Current.GoToAsync("..");
    }
	async void OnEliminarClic(object sender, EventArgs e)
	{
		await conexionDatos.DeletePlatoAsync(plato.Id);
        await Shell.Current.GoToAsync("..");
    }
}