function mudarEstado(el) {
  var cadastrar = document.getElementById("cadastrar").style.display;
  var consultar = document.getElementById("consultar").style.display;

  if (el == "cadastrar") {
    if (cadastrar == "none") {
      document.getElementById("cadastrar").style.display = "block";
      document.getElementById("consultar").style.display = "none";
    }
  } else {
    if (el == "consultar") {
      if (consultar == "none") {
        document.getElementById("consultar").style.display = "block";
        document.getElementById("cadastrar").style.display = "none";
      }
    }
  }
}

async function getFundacoes() {
  var ajax = new XMLHttpRequest();
  ajax.open("GET", "https://localhost:44319/fundacao", true);
  ajax.responseType = "json";
  ajax.send();
  ajax.addEventListener("readystatechange", function () {
    if (ajax.readyState === 4 && ajax.status === 200) {
      var fundacoes = ajax.response;

      if ($("#fundacoesTable tbody").length == 0) {
        $("#fundacoesTable").append("<tbody></tbody>");
      }
      
      if (fundacoes.length == 0) {
        document.getElementById("tableNull").style.display = "block";
      } else {
        document.getElementById("tableNull").style.display = "none";
        for (var i = 0; i < fundacoes.length; i++) {
          $("#fundacoesTable tbody").append(
            "<tr>" +
              "<td>" +
              fundacoes[i].id +
              "</td>" +
              "<td>" +
              fundacoes[i].nome +
              "</td>" +
              "<td>" +
              fundacoes[i].cnpj +
              "</td>" +
              "<td>" +
              fundacoes[i].email +
              "</td>" +
              "<td>" +
              fundacoes[i].telefone +
              "</td>" +
              "<td>" +
              fundacoes[i].instituicaoDeApoio +
              "</td>" +
              "<td>" +
              "<button type='button' " +
              "onclick='fundacaoDisplay(this);' " +
              "class='btn btn-default'>" +
              "<span class='glyphicon glyphicon-edit'></span>" +
              "</button>" +
              "</td>" +
              "<td>" +
              "<button " +
              "onclick='remover(this);' " +
              "class='btn btn-default'>" +
              "<span class='glyphicon glyphicon-remove'></span>" +
              "</button>" +
              "</td>" +
              "</tr>"
          );
        }
    }
    }
  });
}

function fundacaoDisplay(button_edit) {
  var _row = null;

  _row = $(button_edit).parents("tr");
  var cols = _row.children("td");

  $("#id").val($(cols[0]).text());
  $("#nome").val($(cols[1]).text());
  $("#cnpj").val($(cols[2]).text());
  $("#email").val($(cols[3]).text());
  $("#telefone").val($(cols[4]).text());
  $("#instituicao").val($(cols[5]).text());

  mudarEstado("cadastrar");
}

function addOrUpdate() {
  var id = $("#id").val();

  if(validarCampos()){
    if (id != "") {
      var fundacao = {
        Id: id * 1,
        Nome: $("#nome").val(),
        CNPJ: $("#cnpj").val(),
        Email: $("#email").val(),
        Telefone: $("#telefone").val(),
        instituicaoDeApoio: $("#instituicao").val(),
      };
  
      console.log(fundacao);
  
      $.ajax({
        type: "PUT",
        url: "https://localhost:44319/fundacao/",
        contentType: "application/json",
        data: JSON.stringify(fundacao),
        success: function () {
          alert("Instituição alterada com sucesso");
          document.location.reload(true);
        },
        error: function(){
          alert("Erro ao cadastrar Fundação. Verifique o CNPJ");
          document.location.reload(true);
        }
      });
    } else {
      var fundacao = {
        Nome: $("#nome").val(),
        CNPJ: $("#cnpj").val(),
        Email: $("#email").val(),
        Telefone: $("#telefone").val(),
        instituicaoDeApoio: $("#instituicao").val(),
      };
  
      $.ajax({
        type: "POST",
        url: "https://localhost:44319/fundacao/",
        contentType: "application/json",
        data: JSON.stringify(fundacao),
        success: function (data) {
          alert("Instituição cadastrada com sucesso");
          limparCampos();
          document.location.reload(true);
        },
        error: function(){
          alert("Erro ao cadastrar Fundação. Verifique o CNPJ");
          //document.location.reload(true);
        }
      });
    }
  }

  //limparCampos();
  // setTimeout(function(){
  //   document.location.reload(true);
  // },5000);
  
}

function remover(button_edit) {
  var _row = null;

  _row = $(button_edit).parents("tr");
  var cols = _row.children("td");

  var id = $(cols[0]).text();

  $.ajax({
    type: "DELETE",
    url: "https://localhost:44319/fundacao/" + id,
    success: function (data) {
      alert("Fundação excluída com sucesso");
      document.location.reload(true);
    },
    error: function(){
      alert("Erro ao remover Fundação. Verifique sua conexão com a internet");
    }
  });

  

}

function validarCampos(){
  var fundacao = {
    Nome: $("#nome").val(),
    CNPJ: $("#cnpj").val(),
    Email: $("#email").val(),
    Telefone: $("#telefone").val(),
    instituicaoDeApoio: $("#instituicao").val(),
  };

  if(fundacao.Nome == ""){
    alert("Preencha o nome");
    return false;
  }

  if(fundacao.CNPJ == ""){
    alert("Preencha o CNPJ");
    return false;
  }

  if(fundacao.Email == ""){
    alert("Preencha o Email");
    return false;
  }

  if(fundacao.Telefone == ""){
    alert("Preencha o Telefone");
    return false;
  }

  if(fundacao.instituicaoDeApoio == ""){
    alert("Preencha a Instituiçao De Apoio");
    return false;
  }

  return true;

}

function limparCampos(){ 
  $("#id").val("-1");
  $("#nome").val("");
  $("#cnpj").val("");
  $("#email").val("");
  $("#telefone").val("");
  $("#instituicao").val("");
}

