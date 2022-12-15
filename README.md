
# :wolf:  GeoPet 🐾 Localization

Esta API tem o intuito de cadastrar pessoas cuidadoras de pets e seus pet ou pets que estão sendo cuidados.
A API também traz funcionalidade de localizar o Pet e mostrar o endereço de onde o mesmo se encontra.


## Funcionalidades

- Para acessar os endpoints é necessário fazer o login e autenticar a rotas com post/login onde irá gerar um token e você conseguirá usar a funcionalidade
- Localização do Pet
- Geração de QR Code
- Cadastro de tutor
- Cadastro de Pet
- Retorno de endereço através de latitude e longitude


## Instalação
**:warning:Antes de instalar verifique se possui o docker e o docker compose instalados em sua maquina.**
<details>
  <summary><strong>🐋 Rodando no Docker</strong></summary>
  Execute:

  ```bash
    docker-compose up
  ```
  Depois basta rodar a aplicação ou digitar:
  ```bash
    dotnet run
  ```
  </details>
  
## Documentação da API

#### Pet

| Method   | Route       | Description                           |
| :---------- | :--------- | :---------------------------------- |
| GET | /Pet | Retorna todos os Pets cadastrados |
| POST | /Pet | Cadastra um Pet na base |
| GET | /Pet/{id} | Retorna um Pet pelo seu Id |
| PATCH | /Pet/{id} | Atualiza informações do seu Pet pelo seu Id |
| DELETE | /Pet/{id} | Deleta um Pet na base através do seu Id |
| GET | /Pet/{id}/QRCode | Retorna um QRCode com informações do pet específico |

#### PetCarer

| Method   | Route       | Description                           |
| :---------- | :--------- | :---------------------------------- |
| GET | /PetCarer | Retorna todos os Cuidadores |
| POST | /PetCarer | Cadastra um Cuidador na base |
| GET | /PetCarer/{id} | Retorna um Cuidador pelo seu Id |
| PATCH | /PetCarer/{id} | Atualiza informações de um Cuidador pelo seu Id |
| DELETE | /PetCarer/{id} | Deleta um Cuidador na base através do seu Id |
| POST | /PetCarer/login | Autentica o usuário na base gerando o token |

#### Search Location

| Method   | Route       | Description                           |
| :---------- | :--------- | :---------------------------------- |
| GET | /Search | Retorna um endereço com base na latitude e longitude |



## Vídeo Apresentação

[Veja a demonstração](https://youtu.be/6iRUxJaEPBw)

## Tecnologias Utilizadas

- C#
- .dotNET 6
- BCrypt
- XUnit Testes
- FluentAssertions
- Mock
- QRCoder para geração de QRCode
- RestSharp para conexões Http em outros endpoints
- NewtonSoft para serilização dos Json's
- Sql Server
- AutoMapper

## Autores
- [@Isa Correia](https://github.com/IsaCorreia)
- [@Lucas Abreu](https://github.com/defreitaslucas)
- [@Pedro Sousa](https://github.com/pedrossdemelo)

