import React, { Component } from 'react';
import { connect } from "react-redux";
import List from './list'
import Form from './form'
import FormPengembalian from './formPengembalian'
import { PAGE_MODE_LIST } from "../../actions/types";
import { backToList } from "../../actions/peminjaman";

class Peminjaman extends Component {
  constructor(props) {
    super(props);

    this.state = {};
    this.props.backToList();
  }

  render() {
    return (
      <>
        {
          (this.props.pageMode === PAGE_MODE_LIST || this.props.pageMode2 === PAGE_MODE_LIST) ?
            <List />
            : (this.props.formId !== "pengembalian") ? <Form /> : <FormPengembalian />
        }
      </>
    );
  }
}

const mapStateToProps = (state) => ({
  data: state.peminjaman.data,
  pageMode: state.peminjaman.pageMode,
  pageMode2: state.pengembalian.pageMode,
  formId: state.peminjaman.formId,
  isLoading: state.peminjaman.isLoading
});

export default connect(
  mapStateToProps,
  {
    backToList
  }
)(Peminjaman);