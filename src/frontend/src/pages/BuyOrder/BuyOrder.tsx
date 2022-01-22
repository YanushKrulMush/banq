import { Button } from "@material-ui/core";
import { useState } from "react";
import BuyOrderDialog from "./BuyOrderDialog";

const BuyOrder = () => {
  const [isOpen, setIsOpen] = useState(false);
  return (

    <>
      <Button
        variant="outlined"
        color="primary"
        onClick={() => setIsOpen(true)}
      >
        Zlecenie kupna
      </Button>
      <BuyOrderDialog
        isOpen={isOpen}
        handleClose={() => setIsOpen(false)}
      />
    </>
  )
}

export default BuyOrder;