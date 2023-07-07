var API_AVION_URL="https://localhost:7105/api/aviones/GetAviones";

function listarAvion() {
    fetch(API_AVION_URL)
      .then((respuesta) => respuesta.json())
      .then((respuesta) => {
        if (!respuesta.ok) {
          alert("Error!!!");
          return;
        }
  
        respuesta.listaAviones.forEach((avion) => {
          document.getElementById("txtModelo").value = avion.modelo;
          document.getElementById("txtCantMotores").value = avion.cantidadMotores;
          document.getElementById("txtCantAsientos").value = avion.cantidadAsientos;
          document.getElementById("txtFabricante").value = avion.marca;
          document.getElementById("txtDatosVarios").value = avion.datosVarios;
        });
      })
      .catch((err) => {
        alert("Algo salió mal!!");
      });
  }
  