import React, { useState, useEffect } from "react";
import "../styles/box-search.scss";

function BoxSearch({onSearch}: {onSearch: (searchTerm: string) => void}) {
  const [searchTerm, setSearchTerm] = useState('');

  const handleSearch = (event: any) => {
    setSearchTerm(event.target.value);
    onSearch(event.target.value);
  };
  return (
    <>
      <form className="box-search">
        <input type="text" name="keySearch" className="box-search__input" placeholder = "Search here..." 
        value={searchTerm}
        onChange={handleSearch}
        />
        <button type="submit" className="box-search__button">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth={1.5}
            stroke="currentColor"
            className="w-6 h-6"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="m21 21-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607Z"
            />
          </svg>
        </button>
      </form>
    </>
  );
}

export default BoxSearch;
