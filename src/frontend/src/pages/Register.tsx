import React from "react";
import { Link, Redirect } from "react-router-dom";
import {
  Box,
  Button,
  Container,
  CssBaseline,
  Grid,
  TextField,
} from "@material-ui/core";
import styled from "styled-components";
import VerticalCenter from "../layout/VerticalCenter";
import { useAuth } from "../providers/AuthProvider";
import { Controller, useForm } from "react-hook-form";

const InnerContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
`;

const formDefaultValues = {
  firstName: "",
  lastName: "",
  email: "",
  password: "",
};

const Register = () => {
  const { handleSubmit, formState, control } = useForm<
    typeof formDefaultValues
    >({
    defaultValues: formDefaultValues,
    mode: "onChange",
  });

  const auth = useAuth();
  // const onSuccess = (
  //   response: GoogleLoginResponse | GoogleLoginResponseOffline
  // ) => {
  //   if ("tokenId" in response) {
  //     auth.signIn(response.tokenId);
  //   }
  // };

  if (auth.user) {
    return <Redirect to="/" />;
  }

  return (
    <VerticalCenter topMargin="30px" bottomMargin="30px" height="100vh">
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <InnerContainer>
          <form onSubmit={handleSubmit(auth.signIn)}>
            <Grid
              container
              direction="column"
              justify="center"
              alignItems="center"
              spacing={5}
            >
              <Box m={1}>
                <Controller
                  render={({ field }) => (
                    <TextField variant="outlined" label="Imię" type="firstName" {...field} />
                  )}
                  name="firstName"
                  rules={{ required: true }}
                  control={control}
                />
              </Box>
              <Box m={1}>
                <Controller
                  render={({ field }) => (
                    <TextField variant="outlined" label="Nazwisko" type="lastName" {...field} />
                  )}
                  name="lastName"
                  rules={{ required: true }}
                  control={control}
                />
              </Box>
              <Box m={1}>
                <Controller
                  render={({ field }) => (
                    <TextField variant="outlined" label="Email" type="email" {...field} />
                  )}
                  name="email"
                  rules={{ required: true }}
                  control={control}
                />
              </Box>
              <Box m={1}>
                <Controller
                  render={({ field }) => (
                    <TextField variant="outlined" label="Hasło" type="password" {...field} />
                  )}
                  name="password"
                  rules={{ required: true }}
                  control={control}
                />
              </Box>
              <Box m={1}>
                <Button type="submit" color="primary">
                  Dalej
                </Button>
              </Box>
            </Grid>
          </form>
        </InnerContainer>
      </Container>
    </VerticalCenter>
  );
};

export default Register;
