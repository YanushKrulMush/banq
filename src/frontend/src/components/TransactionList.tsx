import useTransactionListQuery, {
  TransactionDetails,
  TransactionType,
} from "../queries/useTransactionListQuery";
import { DataGrid, GridColDef } from "@material-ui/data-grid";
import { format, parseISO } from "date-fns";
import { Box, Container } from "@material-ui/core";

const columns: GridColDef[] = [
  {
    field: "parsedAmount",
    headerName: "Kwota",
    width: 190,
    valueGetter: (params) =>
      `${params.getValue(params.row.id, "amount") ?? 0 / 100} ${
        params.getValue(params.row.id, "currency") ?? ""
      }`,
  },
  {
    field: "details",
    headerName: "Rodzaj i dane transakcji",
    width: 190,
    // valueGetter: (params) => "qwe",
    renderCell: (params) => (
      <DetailsCell
        type={params.row.type}
        description={params.row.description}
      />
    ),
  },
  {
    field: "date",
    headerName: "Data operacji",
    width: 180,
    valueFormatter: (params) => dateFormatter(String(params.value)),
  },
  {
    field: "receiverOrSender",
    headerName: "Nadawca / Odbiorca",
    width: 190,
  },
];

const dateFormatter = (date: string) =>
  format(parseISO(date), "dd-MM-yyyy H:mm");

const TransactionTypeMap = {
  [TransactionType.incomingTransfer]: "Przelew przychodzący",
  [TransactionType.outgoingTransfer]: "Przelew wychodzący",
}

const DetailsCell = ({
  type,
  description,
}: {
  type: TransactionType;
  description: string | null;
}) => (
    <>
      <div>
        <strong>{TransactionTypeMap[type]}</strong>
      </div>
      {/* {description && <div>{description}</div>} */}
    </>
  );

const TransactionList = () => {
  const { data, isLoading } = useTransactionListQuery();

  if (!data || isLoading) {
    return null;
  }

  return (
    <Box height={500} width="100%" paddingTop={4}>
      <DataGrid
        columns={columns}
        rows={data.items}
        disableColumnMenu
        disableDensitySelector
        rowsPerPageOptions={[]}
        hideFooterSelectedRowCount
        // disableMultipleSelection
        // onRowSelected={(selection) => {
        //   setSelectedRow(selection.data.id);
        // }}
      />
    </Box>
  );
};

export default TransactionList;
