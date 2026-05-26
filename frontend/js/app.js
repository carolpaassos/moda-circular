const API_URL = "http://localhost:5222";

const conteudo = document.getElementById("conteudo");
const mensagem = document.getElementById("mensagem");

document.getElementById("btnListarRoupas").addEventListener("click", listarRoupas);
document.getElementById("btnCadastrarRoupa").addEventListener("click", () => exibirFormularioRoupa());
document.getElementById("btnCategorias").addEventListener("click", listarCategorias);

function exibirMensagem(texto, erro = false) {
  mensagem.textContent = texto;
  mensagem.className = erro ? "mensagem erro" : "mensagem";

  setTimeout(() => {
    mensagem.className = "mensagem oculto";
  }, 3500);
}

async function buscarCategorias() {
  const resposta = await fetch(`${API_URL}/categorias`);

  if (!resposta.ok) {
    throw new Error("Erro ao buscar categorias.");
  }

  return await resposta.json();
}

async function listarCategorias() {
  try {
    const categorias = await buscarCategorias();

    conteudo.innerHTML = `
      <div class="grid">
        ${
          categorias.length === 0
            ? `<p class="vazio">Nenhuma categoria cadastrada.</p>`
            : categorias.map(categoria => `
                <div class="card categoria-card">
                  <h3>✧ ${categoria.nome}</h3>
                  <p>${categoria.descricao || "Sem descrição."}</p>
                  <p><strong>Status:</strong> ${categoria.ativa ? "Ativa" : "Inativa"}</p>
                </div>
              `).join("")
        }
      </div>
    `;
  } catch (error) {
    exibirMensagem("Erro ao carregar categorias.", true);
  }
}

async function listarRoupas() {
  try {
    const resposta = await fetch(`${API_URL}/roupas`);

    if (!resposta.ok) {
      throw new Error("Erro ao buscar roupas.");
    }

    const roupas = await resposta.json();

    conteudo.innerHTML = `
      <div class="filtros">
        <div class="campo">
          <label for="filtroCidade">Cidade</label>
          <input id="filtroCidade" placeholder="Ex: Belo Horizonte" />
        </div>

        <div class="campo">
          <label for="filtroTipo">Tipo</label>
          <select id="filtroTipo">
            <option value="">Todos</option>
            <option value="Doação">Doação</option>
            <option value="Troca">Troca</option>
          </select>
        </div>

        <div class="campo">
          <label for="filtroTamanho">Tamanho</label>
          <input id="filtroTamanho" placeholder="Ex: M" />
        </div>

        <div class="campo">
          <label>&nbsp;</label>
          <button id="btnFiltrar">Filtrar</button>
        </div>
      </div>

      <div id="listaRoupas" class="grid">
        ${renderizarCardsRoupas(roupas)}
      </div>
    `;

    document.getElementById("btnFiltrar").addEventListener("click", filtrarRoupas);
  } catch (error) {
    exibirMensagem("Erro ao carregar roupas. Verifique se a API está rodando.", true);
  }
}

function renderizarCardsRoupas(roupas) {
  if (roupas.length === 0) {
    return `<p class="vazio">Nenhuma roupa encontrada.</p>`;
  }

  return roupas.map(roupa => `
    <div class="card card-roupa">
      ${
        roupa.imagemUrl
          ? `<img class="imagem-roupa" src="${roupa.imagemUrl}" alt="${roupa.titulo}" />`
          : ""
      }

      <h3>${roupa.titulo}</h3>

      <p><strong>Tipo:</strong> ${roupa.tipo}</p>
      <p><strong>Tamanho:</strong> ${roupa.tamanho}</p>
      <p><strong>Estado:</strong> ${roupa.estadoConservacao}</p>
      <p><strong>Local:</strong> ${roupa.bairro}, ${roupa.cidade} - ${roupa.estado}</p>
      <p><strong>Disponível:</strong> ${roupa.disponivel ? "Sim" : "Não"}</p>

      <div class="acoes">
        <button onclick="verDetalhesRoupa('${roupa.id}')">Detalhes</button>
        <button class="btn-secundario" onclick="exibirFormularioRoupa('${roupa.id}')">Editar</button>
        <button class="btn-perigo" onclick="removerRoupa('${roupa.id}')">Excluir</button>
      </div>
    </div>
  `).join("");
}

async function filtrarRoupas() {
  const cidade = document.getElementById("filtroCidade").value.trim();
  const tipo = document.getElementById("filtroTipo").value;
  const tamanho = document.getElementById("filtroTamanho").value.trim();

  const parametros = new URLSearchParams();

  if (cidade) parametros.append("cidade", cidade);
  if (tipo) parametros.append("tipo", tipo);
  if (tamanho) parametros.append("tamanho", tamanho);

  try {
    const resposta = await fetch(`${API_URL}/roupas?${parametros.toString()}`);

    if (!resposta.ok) {
      throw new Error("Erro ao filtrar roupas.");
    }

    const roupas = await resposta.json();

    document.getElementById("listaRoupas").innerHTML = renderizarCardsRoupas(roupas);
  } catch (error) {
    exibirMensagem("Erro ao filtrar roupas.", true);
  }
}

async function verDetalhesRoupa(id) {
  try {
    const resposta = await fetch(`${API_URL}/roupas/${id}`);

    if (!resposta.ok) {
      exibirMensagem("Roupa não encontrada.", true);
      return;
    }

    const roupa = await resposta.json();

    conteudo.innerHTML = `
      <div class="card">
        ${
          roupa.imagemUrl
            ? `<img class="detalhe-imagem" src="${roupa.imagemUrl}" alt="${roupa.titulo}" />`
            : ""
        }

        <h2>${roupa.titulo}</h2>

        <p><strong>Descrição:</strong> ${roupa.descricao}</p>
        <p><strong>Tipo:</strong> ${roupa.tipo}</p>
        <p><strong>Tamanho:</strong> ${roupa.tamanho}</p>
        <p><strong>Estado de conservação:</strong> ${roupa.estadoConservacao}</p>
        <p><strong>Gênero:</strong> ${roupa.genero}</p>
        <p><strong>Localização:</strong> ${roupa.bairro}, ${roupa.cidade} - ${roupa.estado}</p>
        <p><strong>Disponível:</strong> ${roupa.disponivel ? "Sim" : "Não"}</p>

        <div class="acoes">
          <button onclick="listarRoupas()">Voltar</button>
          <button class="btn-secundario" onclick="exibirFormularioRoupa('${roupa.id}')">Editar</button>
        </div>
      </div>
    `;
  } catch (error) {
    exibirMensagem("Erro ao carregar detalhes da roupa.", true);
  }
}

async function exibirFormularioRoupa(id = null) {
  try {
    const categorias = await buscarCategorias();
    let roupa = null;

    if (id) {
      const resposta = await fetch(`${API_URL}/roupas/${id}`);

      if (!resposta.ok) {
        exibirMensagem("Roupa não encontrada.", true);
        return;
      }

      roupa = await resposta.json();
    }

    conteudo.innerHTML = `
      <form id="formRoupa" class="formulario">
        <h2>${id ? "Editar roupa" : "Cadastrar roupa"}</h2>

        <div class="campo">
          <label for="titulo">Título</label>
          <input id="titulo" required value="${roupa?.titulo || ""}" />
        </div>

        <div class="campo">
          <label for="descricao">Descrição</label>
          <textarea id="descricao" required>${roupa?.descricao || ""}</textarea>
        </div>

        <div class="campo">
          <label for="imagemUrl">Imagem</label>
          <input id="imagemUrl" placeholder="Ex: img/camiseta_branca.jpg" value="${roupa?.imagemUrl || ""}" />
        </div>

        <div class="campo">
          <label for="tipo">Tipo</label>
          <select id="tipo" required>
            <option value="Doação" ${roupa?.tipo === "Doação" ? "selected" : ""}>Doação</option>
            <option value="Troca" ${roupa?.tipo === "Troca" ? "selected" : ""}>Troca</option>
          </select>
        </div>

        <div class="campo">
          <label for="tamanho">Tamanho</label>
          <input id="tamanho" required value="${roupa?.tamanho || ""}" />
        </div>

        <div class="campo">
          <label for="estadoConservacao">Estado de conservação</label>
          <select id="estadoConservacao" required>
            <option value="Ótimo" ${roupa?.estadoConservacao === "Ótimo" ? "selected" : ""}>Ótimo</option>
            <option value="Bom" ${roupa?.estadoConservacao === "Bom" ? "selected" : ""}>Bom</option>
            <option value="Regular" ${roupa?.estadoConservacao === "Regular" ? "selected" : ""}>Regular</option>
          </select>
        </div>

        <div class="campo">
          <label for="genero">Gênero</label>
          <select id="genero" required>
            <option value="Unissex" ${roupa?.genero === "Unissex" ? "selected" : ""}>Unissex</option>
            <option value="Masculino" ${roupa?.genero === "Masculino" ? "selected" : ""}>Masculino</option>
            <option value="Feminino" ${roupa?.genero === "Feminino" ? "selected" : ""}>Feminino</option>
            <option value="Infantil" ${roupa?.genero === "Infantil" ? "selected" : ""}>Infantil</option>
          </select>
        </div>

        <div class="campo">
          <label for="estado">Estado</label>
          <input id="estado" required value="${roupa?.estado || "MG"}" />
        </div>

        <div class="campo">
          <label for="cidade">Cidade</label>
          <input id="cidade" required value="${roupa?.cidade || "Belo Horizonte"}" />
        </div>

        <div class="campo">
          <label for="bairro">Bairro</label>
          <input id="bairro" required value="${roupa?.bairro || ""}" />
        </div>

        <div class="campo">
          <label for="categoriaId">Categoria</label>
          <select id="categoriaId" required>
            ${categorias.map(categoria => `
              <option value="${categoria.id}" ${roupa?.categoriaId === categoria.id ? "selected" : ""}>
                ${categoria.nome}
              </option>
            `).join("")}
          </select>
        </div>

        <div class="campo">
          <label for="disponivel">Disponível</label>
          <select id="disponivel">
            <option value="true" ${roupa?.disponivel !== false ? "selected" : ""}>Sim</option>
            <option value="false" ${roupa?.disponivel === false ? "selected" : ""}>Não</option>
          </select>
        </div>

        <button type="submit">${id ? "Salvar alterações" : "Cadastrar"}</button>
      </form>
    `;

    document.getElementById("formRoupa").addEventListener("submit", (event) => {
      salvarRoupa(event, id);
    });
  } catch (error) {
    exibirMensagem("Erro ao carregar formulário de roupa.", true);
  }
}

async function salvarRoupa(event, id = null) {
  event.preventDefault();

  const roupa = {
    titulo: document.getElementById("titulo").value.trim(),
    descricao: document.getElementById("descricao").value.trim(),
    imagemUrl: document.getElementById("imagemUrl").value.trim(),
    tipo: document.getElementById("tipo").value,
    tamanho: document.getElementById("tamanho").value.trim(),
    estadoConservacao: document.getElementById("estadoConservacao").value,
    genero: document.getElementById("genero").value,
    estado: document.getElementById("estado").value.trim(),
    cidade: document.getElementById("cidade").value.trim(),
    bairro: document.getElementById("bairro").value.trim(),
    categoriaId: document.getElementById("categoriaId").value,
    disponivel: document.getElementById("disponivel").value === "true"
  };

  const url = id ? `${API_URL}/roupas/${id}` : `${API_URL}/roupas`;
  const metodo = id ? "PUT" : "POST";

  try {
    const resposta = await fetch(url, {
      method: metodo,
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(roupa)
    });

    if (!resposta.ok) {
      const erro = await resposta.json();
      exibirMensagem(erro.mensagem || "Erro ao salvar roupa.", true);
      return;
    }

    exibirMensagem(id ? "Roupa atualizada com sucesso." : "Roupa cadastrada com sucesso.");
    listarRoupas();
  } catch (error) {
    exibirMensagem("Erro ao salvar roupa.", true);
  }
}

async function removerRoupa(id) {
  const confirmar = confirm("Deseja realmente excluir esta roupa?");

  if (!confirmar) {
    return;
  }

  try {
    const resposta = await fetch(`${API_URL}/roupas/${id}`, {
      method: "DELETE"
    });

    if (!resposta.ok) {
      exibirMensagem("Erro ao excluir roupa.", true);
      return;
    }

    exibirMensagem("Roupa excluída com sucesso.");
    listarRoupas();
  } catch (error) {
    exibirMensagem("Erro ao excluir roupa.", true);
  }
}

listarRoupas();