const URL = 'https://localhost:7105/api/aviones/GetAviones'

function listarAviones(){
       
    fetch(URL)
    .then((respuesta) => respuesta.json())
    .then((respuesta) => {
        if(!respuesta.ok){
            alert("Error!!!")
            return;
        }

        const cuerpoTabla = document.querySelector('tbody')

        respuesta.listaAviones.forEach((av) => {
            const fila = document.createElement('tr')
            if(av.id)
            {
                fila.innerHTML += `<td>${av.id}</td>`               
            }
            else{
                alert("No se dispone del ID");
            }
            if(av.idFabricante)
            {
                fila.innerHTML += `<td>${av.idFabricante}</td>`               
            }
            else{
                alert("No se dispone del ID del fabricante");
            }
            if(av.modelo)
            {
                fila.innerHTML += `<td>${av.modelo}</td>`               
            }
            else{
                alert("No se dispone del Modelo");
            }
            if(av.marca)
            {
                fila.innerHTML += `<td>${av.marca}</td>`               
            }
            else{
                alert("No se dispone de la Marca");
            }
            if(av.cantidadAsientos)
            {
                fila.innerHTML += `<td>${av.cantidadAsientos}</td>`               
            }
            else{
                alert("No se dispone de la cantidad de asientos");
            }
            if(av.cantidadMotores)
            {
                fila.innerHTML += `<td>${av.cantidadMotores}</td>`               
            }
            else{
                alert("No se dispone de la cantidad de asientos");
            }
            if(av.datosVarios)
            {
                fila.innerHTML += `<td>${av.datosVarios}</td>`               
            }
            else{
                alert("No se dispone de la cantidad de asientos");
            }

            cuerpoTabla.append(fila)
        });
    }).catch((err) => {
        alert("Algo salio mal!!")
    })
}

