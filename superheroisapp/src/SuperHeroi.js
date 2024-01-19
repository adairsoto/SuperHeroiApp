import axios from "axios";
import React, { Fragment, useEffect, useState } from "react";
import { Button, Col, Container, Modal, Row } from "react-bootstrap";
import Table from "react-bootstrap/Table";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Form from "react-bootstrap/Form";

const SuperHeroi = () => {
  const [validated, setValidated] = useState(false);

  const [show, setShow] = useState(false);
  const [action, setAction] = useState(false);
  const handleShow = () => setShow(true);
  const handleClose = () => {
    setShow(false);
    clearFields();
  };

  const [idHeroi, setIdHeroi] = useState("");
  const [nome, setNome] = useState("");
  const [nomeHeroi, setNomeHeroi] = useState("");
  const [dataNascimento, setDataNascimento] = useState("");
  const [altura, setAltura] = useState("");
  const [peso, setPeso] = useState("");

  const [data, setData] = useState([]);
  const [superPoderes, setSuperPoderes] = useState([]);
  const [superPoderesIds, setSuperPoderesIds] = useState([]);

  const handleSubmit = (event) => {
    const form = event.currentTarget;

    if (form.checkValidity()) {
      if (action === "Criar") handleSave();
      else handleUpdate();
    }
    event.preventDefault();
    setValidated(true);
  };

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = () => {
    axios
      .get("https://localhost:7281/api/SuperHeroi")
      .then((result) => {
        setData(result.data);
      })
      .catch((error) => {
        toast.error(error.response.data);
      });
  };

  const getSuperPoderes = () => {
    axios
      .get("https://localhost:7281/api/SuperHeroi/superPoderes")
      .then((result) => {
        setSuperPoderes(result.data);
      })
      .catch((error) => {
        toast.error(error.response.data);
      });
  };

  const handleCreate = () => {
    handleShow();
    setAction("Criar");
    getSuperPoderes();
  };

  const handleSave = () => {
    const url = "https://localhost:7281/api/SuperHeroi";
    const data = {
      nome: nome,
      nomeHeroi: nomeHeroi,
      dataNascimento: dataNascimento,
      altura: altura,
      peso: peso,
      superPoderesIds: superPoderesIds,
    };

    axios
      .post(url, data)
      .then((result) => {
        fetchData();
        handleClose();
        toast.success(result.data);
      })
      .catch((error) => {
        toast.error(error.response.data);
      });
  };

  const handleEdit = (id) => {
    getSuperPoderes();
    handleShow();
    setAction("Editar");
    axios
      .get(`https://localhost:7281/api/SuperHeroi/${id}`)
      .then((result) => {
        setIdHeroi(result.data.id);
        setNome(result.data.nome);
        setNomeHeroi(result.data.nomeHeroi);
        setDataNascimento(result.data.dataNascimento.slice(0, 10));
        setAltura(result.data.altura);
        setPeso(result.data.peso);
      })
      .catch((error) => {
        toast.error(error.response.data);
      });
  };

  const handleDelete = (id) => {
    if (window.confirm("Você deseja realmente remover esse herói?") === true) {
      axios
        .delete(`https://localhost:7281/api/SuperHeroi/${id}`)
        .then((result) => {
          if (result.status === 200) {
            fetchData();
            toast.success(result.data);
          }
        })
        .catch((error) => {
          toast.error(error.response.data);
        });
    }
  };

  const handleUpdate = () => {
    const url = `https://localhost:7281/api/SuperHeroi/${idHeroi}`;
    const data = {
      id: idHeroi,
      nome: nome,
      nomeHeroi: nomeHeroi,
      dataNascimento: dataNascimento,
      altura: altura,
      peso: peso,
      superPoderesIds: [1, 2],
    };

    axios
      .put(url, data)
      .then((result) => {
        handleClose();
        fetchData();
        toast.success(result.data);
      })
      .catch((error) => {
        toast.error(error.response.data);
      });
  };

  const handleSuperPoderes = (e, id) => {
    let spIds = superPoderesIds;
    if (e.target.checked === true) {
      spIds.push(id);
    } else {
      let index = spIds.indexOf(id);
      console.log(index);
      spIds.splice(index, 1);
    }
    setSuperPoderesIds(spIds);
  };

  const clearFields = () => {
    setNome("");
    setNomeHeroi("");
    setDataNascimento("");
    setAltura("");
    setPeso("");
    setSuperPoderesIds([]);
    setValidated(false);
  };

  return (
    <Fragment>
      <ToastContainer />
      <Container className="mb-3 d-flex">
        <button className="btn btn-primary" onClick={() => handleCreate()}>
          Criar Herói
        </button>
      </Container>
      <Table bordered hover>
        <thead>
          <tr>
            <th>#</th>
            <th>Nome</th>
            <th>Nome do Herói</th>
            <th>Altura</th>
            <th>Peso</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {data.map((heroi, index) => {
            return (
              <tr key={index}>
                <td>{heroi.id}</td>
                <td>{heroi.nome}</td>
                <td>{heroi.nomeHeroi}</td>
                <td>{heroi.altura}</td>
                <td>{heroi.peso}</td>
                <td colSpan={2}>
                  <button
                    className="btn btn-primary"
                    onClick={() => handleEdit(heroi.id)}
                  >
                    Editar
                  </button>{" "}
                  &nbsp;
                  <button
                    className="btn btn-danger"
                    onClick={() => handleDelete(heroi.id)}
                  >
                    Remover
                  </button>
                </td>
              </tr>
            );
          })}
        </tbody>
      </Table>
      <Modal show={show} onHide={handleClose}>
        <Form noValidate validated={validated} onSubmit={handleSubmit}>
          <Modal.Header closeButton>
            <Modal.Title>{action} Herói</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Row className="mb-3">
              <Form.Group as={Col} md="6" controlId="validationNome">
                <Form.Label>Nome</Form.Label>
                <Form.Control
                  required
                  type="text"
                  placeholder="Nome"
                  value={nome}
                  onChange={(e) => setNome(e.target.value)}
                />
                <Form.Control.Feedback type="invalid">
                  Campo obrigatório!
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group as={Col} md="6" controlId="validationNomeHeroi">
                <Form.Label>Nome do herói</Form.Label>
                <Form.Control
                  required
                  type="text"
                  placeholder="Nome do herói"
                  value={nomeHeroi}
                  onChange={(e) => setNomeHeroi(e.target.value)}
                />
                <Form.Control.Feedback type="invalid">
                  Campo obrigatório!
                </Form.Control.Feedback>
              </Form.Group>
            </Row>
            <Row className="mb-3">
              <Form.Group as={Col} md="5" controlId="validationDataNascimento">
                <Form.Label>Data de Nascimento</Form.Label>
                <Form.Control
                  type="date"
                  placeholder="Data de Nascimento"
                  value={dataNascimento}
                  onChange={(e) => setDataNascimento(e.target.value)}
                />
              </Form.Group>
              <Form.Group as={Col} md="3" controlId="validationAltura">
                <Form.Label>Altura</Form.Label>
                <Form.Control
                  required
                  type="text"
                  placeholder="Altura"
                  value={altura}
                  onChange={(e) => setAltura(e.target.value)}
                />
                <Form.Control.Feedback type="invalid">
                  Campo obrigatório!
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group as={Col} md="3" controlId="validationPeso">
                <Form.Label>Peso</Form.Label>
                <Form.Control
                  required
                  type="text"
                  placeholder="Peso"
                  value={peso}
                  onChange={(e) => setPeso(e.target.value)}
                />
                <Form.Control.Feedback type="invalid">
                  Campo obrigatório!
                </Form.Control.Feedback>
              </Form.Group>
            </Row>
            {action === "Criar" ? (
              <Form.Group>
                <h5 className="mb-3">Super Poderes</h5>
                {superPoderes.map((superPoder, index) => {
                  return (
                    <Form.Check
                      key={index}
                      type="switch"
                      id={superPoder.id}
                      label={superPoder.superPoder}
                      onChange={(e) => handleSuperPoderes(e, superPoder.id)}
                    />
                  );
                })}
              </Form.Group>
            ) : (
              ""
            )}
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={() => handleClose()}>
              Cancelar
            </Button>
            <Button variant="primary" type="submit">
              Salvar
            </Button>
          </Modal.Footer>
        </Form>
      </Modal>
    </Fragment>
  );
};

export default SuperHeroi;
