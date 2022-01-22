import { Grid } from "@material-ui/core";
import useAccountQuery, { Account } from "../queries/useAccountQuery";

import styled from "styled-components";

type Props = {
  name: keyof Account;
  value: string;
};

const accountMap = {
  number: "Numer rachunku",
  balance: "Saldo rachunku",
  currency: "Waluta",
  openedOn: "Data otwarcia rachunku",
};

const RightAlignedGrid = styled(Grid)`
  text-align: right;
`;

const GridItem = ({ name, value }: Props) => (
  <Grid container item xs={12} spacing={3}>
    <RightAlignedGrid item xs={5}>
      {accountMap[name]}:
    </RightAlignedGrid>
    <Grid item xs={7}>
      {value}
    </Grid>
  </Grid>
);

const AccountDetails = () => {
  const { data, isLoading } = useAccountQuery();

  if (!data || isLoading) {
    return null;
  }

  return (
    <Grid container spacing={1}>
      {Object.entries(data).map(([name, value]) => (
        <GridItem name={name as keyof Account} value={value.toString()} />
      ))}
    </Grid>
  );
};

export default AccountDetails;
