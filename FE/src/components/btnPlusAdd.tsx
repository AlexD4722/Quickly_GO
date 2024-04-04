import React, { useState, useEffect } from "react";
import "../styles/button-add.scss";
function BtnPlusAdd() {
  return (
    <>
      <div className="btn-add">
        <button className="btn btn-primary btn-add__box"></button>
      </div>
    </>
  );
}

export default BtnPlusAdd;
