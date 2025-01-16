
# **LivrariaDesafio**

## 📚 **Descrição do Projeto**
O **LivrariaDesafio** é uma aplicação web desenvolvida como parte de um desafio técnico, que tem como objetivo gerenciar o cadastro de livros, autores e assuntos. Além disso, o projeto foi projetado para incluir uma funcionalidade de precificação dinâmica para vendas em diferentes modos de compra.

---

## 🚀 **Funcionalidades Implementadas**
### 🖥️ **Backend - API REST**
- **Estrutura em Camadas**:
  - **Domain**: Regras de negócio, entidades, validadores.
  - **Application**: Use cases e interfaces.
  - **Infrastructure**: Persistência de dados com EF Core.
  - **API**: Exposição de endpoints REST.

- **Endpoints disponíveis**:
  - **Livros (Books)**:
    - `GET /api/v1/books` - Retorna todos os livros cadastrados.
    - `GET /api/v1/books/{id}` - Detalhes de um livro específico.
    - `POST /api/v1/books` - Adiciona um novo livro.
    - `PUT /api/v1/books/{id}` - Atualiza um livro existente.
    - `DELETE /api/v1/books/{id}` - Remove um livro.

  - **Autores (Authors)**:
    - `GET /api/v1/authors` - Lista todos os autores.
    - `GET /api/v1/authors/{id}` - Detalhes de um autor.

  - **Assuntos (Subjects)**:
    - `GET /api/v1/subjects` - Lista todos os assuntos.
    - `GET /api/v1/subjects/{id}` - Detalhes de um assunto.

  - **Relatórios**:
    - `GET /api/v1/reports/books-with-authors` - Gera relatório de livros agrupados por autores.

### 🎨 **Frontend - ASP.NET MVC**
- **Estrutura**:
  - Projeto desenvolvido em ASP.NET MVC utilizando **.NET 8**.
  - Layout responsivo com **Bootstrap** e navegação por abas (com suporte a atalhos de teclado).
  
- **Funcionalidades**:
  - **Listagem de Livros, Autores e Assuntos**:
    - Exibição em tabelas responsivas.
  - **CRUD de Livros**:
    - Adicionar, editar e excluir livros.
  - **Relatórios**:
    - Visualização de dados agrupados por autor.

---

## 🛠️ **Estrutura do Projeto**
```
solution/
├── src/
│   ├── Application/       <-- Use Cases, Models e Interfaces
│   ├── Domain/            <-- Entidades, Validadores e Regras de Negócio
│   ├── Infrastructure/    <-- Persistência de Dados (EF Core)
│   ├── API/               <-- Endpoints da API REST
│   ├── FrontendApp/       <-- Aplicação ASP.NET MVC
├── tests/
│   ├── UnitTests/         <-- Testes Unitários
├── solution.sln
```

---

## ⚡ **Desafios Enfrentados**
1. **Gestão do Tempo**:
   - Apesar de uma boa organização inicial, o tempo limitado dificultou a implementação de todas as funcionalidades planejadas.

2. **Precificação de Livros**:
   - A lógica de precificação baseada no modo de compra foi planejada, mas não foi possível implementar no prazo. Esta funcionalidade será adicionada em uma futura iteração.

3. **Relatórios Complexos**:
   - O agrupamento de livros por autor foi implementado de maneira básica. Uma integração com um gerador de relatórios mais avançado pode ser feita futuramente.

---

## 💡 **Oportunidades de Melhoria**
- **Implementar a lógica de precificação**:
  - Baseada nos modos de compra (`balcão`, `internet`, `evento`), com desconto dinâmico.
  
- **Aprimorar a interface do usuário**:
  - Adicionar mais estilos ao layout utilizando bibliotecas como **Material Design** ou **Tailwind CSS**.

- **Adicionar autenticação e autorização**:
  - Proteger endpoints sensíveis com autenticação por JWT.

- **Melhoria nos Relatórios**:
  - Utilizar ferramentas como **ReportViewer** ou **Crystal Reports** para relatórios detalhados.

- **Testes Automatizados**:
  - Expandir a cobertura de testes unitários e adicionar testes de integração.

---

## 📑 **Como Rodar o Projeto**
### **Pré-requisitos**
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server)

### **Passos**
1. Clone o repositório:
   ```bash
   git clone https://github.com/filipemgomes/LivrariaDesafio.git
   cd LivrariaDesafio
   ```

2. Configure o banco de dados:
   - Atualize a string de conexão no arquivo `appsettings.json` do projeto **API**.

3. Execute as migrações para criar o banco de dados:
   ```bash
   cd src/Infrastructure
   dotnet ef database update
   ```

4. Inicie a API:
   ```bash
   cd src/API
   dotnet run
   ```

5. Inicie o frontend:
   ```bash
   cd src/FrontendApp
   dotnet run
   ```

6. Acesse os serviços:
   - API: [https://localhost:7238/swagger](https://localhost:7238/swagger)
   - Frontend: [https://localhost:7109](https://localhost:7109)

---

## ✨ **Conclusão**
O **LivrariaDesafio** é uma base sólida para sistemas de gestão de livrarias, demonstrando boas práticas de arquitetura e desenvolvimento. Apesar do prazo limitado, foi possível implementar funcionalidades essenciais e criar uma estrutura escalável para melhorias futuras.
