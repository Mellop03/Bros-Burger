var parametros = new URLSearchParams(window.location.search)
var acaoDoBotao = parametros.get("botao")
if (acaoDoBotao == "relatorio"){
    var escruvinhar = parametros.get("txtFiltro")
    var escruvinhar2 = parametros.get("selOrdenacao")
    switch(escruvinhar2){
        case "Nome": document.getElementById("H1Filtro").innerHTML += `Ordenado por nome do produto`; break  
        case "Nome_Desc": document.getElementById("H1Filtro").innerHTML += `Ordenado por nome do produto decrescente`;break  
        case "Categoria": document.getElementById("H1Filtro").innerHTML += `Ordenado por categoria`;  break
    }
    if (escruvinhar == null || escruvinhar == ""){
        console.log("Nada")
    } else {
        document.getElementById("H1Filtro").innerHTML += ` | Consulta: ${escruvinhar}`
    }
    window.print()
}