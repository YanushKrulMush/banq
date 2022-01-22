import { DataGrid, GridColDef } from "@material-ui/data-grid";
import { Box } from "@material-ui/core";
import useStocksListQuery from "../queries/useStocksListQuery";

const columns: GridColDef[] = [
  {
    field: "name",
    headerName: "Nazwa",
    width: 190,
  },
  {
    field: "value",
    headerName: "Wartość",
    width: 130,
    valueFormatter: (params) => (String((Number(params.value) ?? 0) / 100)),
  },
  {
    field: "quantity",
    headerName: "Ilość",
    width: 100,
  },
  {
    field: "total",
    headerName: "Łączna wartość",
    width: 190,
    valueFormatter: (params) => (String((Number(params.value) ?? 0) / 100)),
  },
];



const StocksList = () => {
  const { data, isLoading } = useStocksListQuery();

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

export default StocksList;
