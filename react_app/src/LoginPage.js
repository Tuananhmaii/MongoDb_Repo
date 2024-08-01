import {
    MDBBtn,
    MDBContainer,
    MDBCard,
    MDBCardBody,
    MDBCardImage,
    MDBRow,
    MDBCol,
    MDBInput,
    MDBCheckbox
  }
  from 'mdb-react-ui-kit';
  
  import React, { useState } from 'react';
  import { Form, Button, Alert } from 'react-bootstrap';
  import axios from 'axios';
  import { useNavigate } from 'react-router-dom';
  
  
  function App() {
  const [email, setEmail] = useState('');
    const [error, setError] = useState(null);
    const navigate = useNavigate();
  
    const handleEmailChange = (e) => {
      setEmail(e.target.value);
    };
  
    const handleSubmit = async (e) => {
      e.preventDefault();
      if (!email) {
        setError('Please enter email address.');
        return; // Exit the function if the email is invalid
      }
      try {
        const response = await axios.get(`https://localhost:7171/api/user/${email}`);
        if (response.status === 200) {
          navigate('/main', { state: { userData: response.data } });
          console.log(response.data)
        }
      } catch (error) {
        setError('Invalid email. Please try again');
      }
    };
  
    return (
      <MDBContainer className='my-5'>
        <MDBCard>
  
          <MDBRow className='g-0 d-flex align-items-center'>
  
            <MDBCol md='4'>
              <MDBCardImage src='https://mdbootstrap.com/img/new/ecommerce/vertical/004.jpg' alt='phone' className='rounded-t-5 rounded-tr-lg-0' fluid />
            </MDBCol>
  
            <MDBCol md='8'>
            <h2 className="fw-bold mb-2 text-center">Sign in</h2>
              <MDBCardBody>
  
                <MDBInput type="email" 
                    placeholder="name@example.com" 
                    value={email}
                    onChange={handleEmailChange}
                    wrapperClass='mb-4'/>
  
              <button type="button" onClick={handleSubmit} class="btn btn-primary btn-lg btn-block" style={{width: '100%'}}>Login</button>
  
              </MDBCardBody>
              {error && <Alert variant="danger">{error}</Alert>}
            </MDBCol>
  
          </MDBRow>
  
        </MDBCard>
      </MDBContainer>
    );
  }
  
  export default App;