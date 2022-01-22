import { useAuth } from "../providers/AuthProvider";
import { Redirect, Route } from "react-router-dom";
import React from "react";
import styled from "styled-components";
import { Box, Container } from "@material-ui/core";
import SignOutButton from "./SignOutButton";
import SideMenu from "../components/SideMenu";

const StyledContainer = styled(Container)<{ component: React.ElementType }>`
  height: 300px;
  margin-top: ${({ theme }) => theme.spacing(3)};
`;

const PrivateRoute = ({
  children,
  ...rest
}: React.PropsWithChildren<unknown> & React.ComponentProps<typeof Route>) => {
  const auth = useAuth();
  if (!children) return null;

  return (
    <Route
      {...rest}
      render={({ location }) =>
        auth.user ? (
          <>
            <Box display="flex" m={2}>
              <Box ml="auto">
                <SignOutButton />
              </Box>
            </Box>
            <SideMenu>{children}</SideMenu>
          </>
        ) : (
          <Redirect
            to={{
              pathname: "/login",
              state: { from: location },
            }}
          />
        )
      }
    />
  );
};

export default PrivateRoute;
