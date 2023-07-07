const URL="https://localhost:7105/api/Aviones/DeleteAviones/";

function del(even){
    even.preventDefault();

    let id= document.getElementById("txtId");

    let valor= id.value;
    console.log(valor);
    if(valor==="")
    {
        alert("El campo ID es obligatorio");
        return;
    }

    const request={
        id:parseInt(valor)
    }
    console.log(request);

    fetch(URL+valor,{
        body:JSON.stringify(request),
        method:"delete",
        headers: {
            'Content-Type': 'application/json'
          }
          
        })
        .then(response => response.json())
        .then(data => {
          if (data.ok) {
            alert('Se Borro correctamente el avion');
          } else {
            alert(data.mensajeError);
          }
        })
        .catch(err => {
          console.error('Error en la solicitud:', err);
          alert('Ocurrió un error al realizar la solicitud. Por favor, intenta nuevamente.');
        });
    }
  
  document.getElementById('BtnDelete').addEventListener('click', del);
