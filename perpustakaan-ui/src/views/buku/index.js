import React, { Component } from 'react';
import { connect } from "react-redux";
import List from './list'
import Form from './form'
import { PAGE_MODE_FORM } from "../../actions/types";
import { backToList } from "../../actions/buku";

class Buku extends Component {
  constructor(props) {
    super(props);

    this.state = {};
    this.props.backToList();
  }

  render() {
    return (
      <>
        {(this.props.pageMode === PAGE_MODE_FORM) ? <Form /> : <List />}
      </>
    );
  }
}

const mapStateToProps = (state) => ({
  data: state.buku.data,
  pageMode: state.buku.pageMode,
  isLoading: state.buku.isLoading
});

export default connect(
  mapStateToProps,
  {
    backToList
  }
)(Buku);