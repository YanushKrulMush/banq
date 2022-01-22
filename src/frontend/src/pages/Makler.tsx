import { Box, Button, Container, Grid, Paper } from "@material-ui/core";
import VerticalCenter from "../layout/VerticalCenter";
import React from "react";
import styled from "styled-components";
import AccountDetails from "../components/AccountDetails";
import TransactionList from "../components/TransactionList";
import NewTransaction from "./NewTransaction/NewTransaction";
import SellOrder from "./SellOrder/SellOrder";
import BuyOrder from "./BuyOrder/BuyOrder";
import StocksList from "../components/StocksList";

const StyledContainer = styled(Container)<{ component: React.ElementType }>`
  min-height: 300px;
`;

const StyledPaper = styled(Paper)`
  padding: ${({ theme }) => theme.spacing(4)};
`;

const Makler = () => (
    <VerticalCenter topMargin="30px" bottomMargin="30px" height="100vh">
      <StyledContainer component="main" maxWidth="lg">
        <Box display="flex" mb={1}>
          <Box mr={1}>
            <BuyOrder />
          </Box>
          <Box>
            <SellOrder />
          </Box>
        </Box>
        <StyledPaper>
          <Grid container direction="row" justify="center" alignItems="center">
            <StocksList />
          </Grid>
        </StyledPaper>
      </StyledContainer>
    </VerticalCenter>
  );

export default Makler;
