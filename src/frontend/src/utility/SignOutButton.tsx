import { Box, Button } from "@material-ui/core";
import React from "react";
import ExitToAppIcon from "@material-ui/icons/ExitToApp";
import { useAuth } from "../providers/AuthProvider";

const SignOutButton = () => {
  const auth = useAuth();

  return (
    <Button variant="outlined" color="primary" onClick={auth.signOut}>
      <ExitToAppIcon fontSize="small" />
      <Box ml={1}>Wyloguj</Box>
    </Button>
  );
};

export default SignOutButton;
